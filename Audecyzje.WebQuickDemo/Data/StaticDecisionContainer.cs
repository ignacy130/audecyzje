using Audecyzje.WebQuickDemo.Models;
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
        private static ConcurrentBag<DecisionTag> _decisionTags;
        private static ConcurrentBag<Decision> _decisions;
        private static ConcurrentBag<Tag> _tags;
        private static string _connectionString;

        private readonly static object _locker = new object();
        private static StaticDecisionContainer _instance;
        private StaticDecisionContainer()
        {
            InitializeBag();
        }
        public static StaticDecisionContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StaticDecisionContainer();
                }
                return _instance;
            }
        }

        public static WarsawContext DbContext
        {
            set
            {
                _realDbContext = value;
            }
        }

        public static ConcurrentBag<DecisionTag> DecisionTags
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


        private static void InitializeBag()
        {
            _decisionTags = new ConcurrentBag<DecisionTag>();
            _decisions = new ConcurrentBag<Decision>();
            _tags = new ConcurrentBag<Tag>();

            
            foreach (var dt in _realDbContext.DecisionTags)
            {
                _decisionTags.Add(dt);
            }
            foreach (var d in _realDbContext.Descisions)
            {
                _decisions.Add(d);
            }
            foreach (var t in _realDbContext.Tags)
            {
                _tags.Add(t);
            }
        }

        public static void RecreateAllTags()
        {            // probably better use SynchronizedCollection 
            _decisionTags = new ConcurrentBag<DecisionTag>();
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
        public static void RecreateSingleTags(int id)
        {
            // probably better use SynchronizedCollection 

        }

        public static void SyncDatabse()
        {
            //todo some magic shit assign to Job
        }

    }
}
