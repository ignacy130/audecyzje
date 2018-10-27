using Audecyzje.WebQuickDemo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Audecyzje.WebQuickDemo.Data
{
    public class StaticDecisionContainer
    {
        private static WarsawContext _realDbContext;
        private SynchronizedCollection<DecisionTag> _decisionTags;
        private SynchronizedCollection<Decision> _decisions;
        private SynchronizedCollection<Tag> _tags;

        private readonly static object _locker = new object();
        private static StaticDecisionContainer _instance;
        private StaticDecisionContainer()
        {
            InitializeBag();
        }
        public static StaticDecisionContainer GetInstance(WarsawContext context)
        {

            if (_instance == null)
            {
                _realDbContext = context;
                _instance = new StaticDecisionContainer();
            }
            return _instance;

        }

        public WarsawContext DbContext
        {
            set
            {
                _realDbContext = value;
            }
        }

        public SynchronizedCollection<DecisionTag> DecisionTags
        {
            get
            {
                lock (_locker)
                {
                    if (_decisionTags == null)
                    {
                        InitializeBag();
                    }
                    return _decisionTags;
                }
            }
        }
        public SynchronizedCollection<Tag> Tags
        {
            get
            {
                lock (_locker)
                {
                    if (_tags == null)
                    {
                        InitializeBag();
                    }
                    return _tags;
                }
            }
        }

        public void AddTag(Tag tag)
        {
            lock (_locker)
            {
                _realDbContext.Tags.Add(tag);
                _realDbContext.SaveChangesAsync();
                _tags.Add(tag);
            }
        }
        public void UpdateTag(Tag newTag)
        {
            var previous = _tags.Single(x => x.ID == newTag.ID);
            if (previous != null)
            {
                _tags.Remove(previous);
                _tags.Add(newTag);
            }
        }
        public void RemoveTag(int id)
        {
            var tag = _tags.Single(x => x.ID == id);
            if (tag != null)
            {
                _tags.Remove(tag);
            }
        }

        private void InitializeBag()
        {
            _decisionTags = new SynchronizedCollection<DecisionTag>();
            _decisions = new SynchronizedCollection<Decision>();
            _tags = new SynchronizedCollection<Tag>();
            if (_realDbContext != null)
            {
                foreach (var dt in _realDbContext?.DecisionTags)
                {
                    _decisionTags.Add(dt);
                }
                foreach (var d in _realDbContext?.Descisions)
                {
                    _decisions.Add(d);
                }
                foreach (var t in _realDbContext?.Tags.Include(t => t.LinkedDecisions).ThenInclude(link => link.Decision))                

                {
                    _tags.Add(t);
                }
            }
        }

        public void RecreateAllTags()
        {
            _decisionTags = new SynchronizedCollection<DecisionTag>();
            Parallel.ForEach(_tags, tag =>
            {
                Parallel.ForEach(_decisions, dec =>
                {
                    var regexp = tag.RegExp;
                    if (Regex.IsMatch(dec.Content, regexp, RegexOptions.IgnoreCase))
                    {
                        DecisionTag dt = new DecisionTag()
                        {
                            DecisionID = dec.ID,
                            TagID = tag.ID
                        };
                        _decisionTags.Add(dt);
                    }
                });
            });
        }
        public void RecreateSingleTag(int? id)
        {
            Tag tag = _tags.Where(x => x.ID == id).SingleOrDefault();
            if (tag != null)
            {
                lock (_locker)
                {
                    var decisions = _decisions.ToList();
                    var existing = _decisionTags.Where(x => x.TagID == id);
                    Parallel.ForEach(existing, existingTag =>
                    {
                        _decisionTags.Remove(existingTag);
                    });

                    Parallel.ForEach(_decisions, dec =>
                    {
                        if (Regex.IsMatch(dec.Content, tag.RegExp, RegexOptions.IgnoreCase))
                        {
                            DecisionTag dt = new DecisionTag()
                            {
                                DecisionID = dec.ID,
                                TagID = tag.ID
                            };
                            _decisionTags.Add(dt);
                        }
                    });
                }
            }
        }

        public async void SaveToDatabase()
        {
            //todo log execution time and warn when exceeding time limit
            foreach (var dt in _realDbContext.DecisionTags)
            {
                _realDbContext.DecisionTags.Remove(dt);
            }
            _realDbContext.DecisionTags.AddRange(_decisionTags);
            foreach (var tag in _realDbContext.Tags)
            {
                _realDbContext.Tags.Remove(tag);
            }
            _realDbContext.Tags.AddRange(_tags);
            await _realDbContext.SaveChangesAsync();
        }


    }
}
