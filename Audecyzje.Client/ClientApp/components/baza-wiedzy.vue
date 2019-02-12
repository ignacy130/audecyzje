<template>
    <div id="knowledge-base" class="subpage-view">
        <div class="broad-view">
            <div class="row mx-4 mx-lg-5 mt-5">
                <div class="definition col-12 col-lg-10 col-xl-7 offset-xl-1 py-5 pl-0">
                    <h2 class="bold-header">
                        Czym jest reprywatyzacja
                    </h2>
                    <p class="copy">
                        Lorem ipsum dolor sit amet, sint tritani pri id, pro veri delenit at, ius ex nibh quaestio. At sit nemore accusata urbanitas, te decore possit pri. Eu placerat neglegentur mei, vel id dicat ancillae. Eos postea prompta id.
                    </p>
                    <div class="quote">
                        <h1>&quot;</h1>
                        <p class="quote-text-small">
                            Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Phasellus enim erat, vestibulum vel, aliquam a, posuere eu, velit. Morbi scelerisque luctus velit. Praesent in mauris eu tortor porttitor accumsan. Mauris dolor felis, sagittis at, luctus sed, aliquam non, tellus. Proin pede metus, vulputate nec, fermentum fringilla, vehicula vitae, justo. In enim a arcu imperdiet malesuada. Vivamus luctus egestas leo.
                        </p>
                    </div>
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row mt-3">
                <div id="posts-search" class="col-2 col-sm-4 col-md-3">
                    <input class="form-control" v-model="query" v-on:keyup="searchKB" placeholder="Wyszukaj artykuł" />
                    <div id="posts-nav">
                        <div v-for="post in currentPosts" v-bind:class="[post.id == selectedArticle.id ? 'link-selected' : '']">
                            <div class="mt-3 mb-1" v-if="post.parentId === -1" v-on:click="selectArticle(post.id)"><strong>{{post.title}}</strong></div>
                            <div class="ml-2 my-1" v-on:click="selectArticle(post.id)" v-else>{{post.title}}</div>
                        </div>
                    </div>
                </div>
                <div class="col-10 col-sm-8  col-md-9">
                    <div v-if="selectedArticle && selectedArticle.id != -1" class="row">
                        <div class="col-12">
                            <h2 class="section-header bold-header my-1">
                                {{selectedArticle.title}}
                            </h2>
                        </div>
                        <div class="col-12 col-lg-12 article-content">
                            <div v-html="selectedArticle.content"></div>
                        </div>
                    </div>
                    <div v-else>
                        <div class="row">
                            <div class="col-12">
                                <h2 class="section-header bold-header mt-1">
                                    Najważniejsze informacje
                                </h2>
                            </div>
                            <div class="col-12 col-lg-6">
                                <div class="fact">
                                    <img src="../img/time-1.svg" alt="time">
                                    <p>
                                        Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Phasellus enim erat, vestibulum vel, aliquam a, posuere eu, velit.
                                    </p>
                                </div>
                            </div>
                            <div class="col-12 col-lg-6">
                                <div class="fact">
                                    <img src="../img/city.svg" alt="city">
                                    <p>
                                        Lorem ipsum dolor sit amet, sint tritani pri id, pro veri delenit at, ius ex nibh quaestio. At sit nemore accusata urbanitas, te decore possit pri. Eu placerat neglegentur mei, vel id dicat ancillae. Eos postea prompta id.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12 col-lg-6">
                                <div class="fact">
                                    <img src="../img/team-1.svg" alt="team">
                                    <p>
                                        Lorem ipsum dolor sit amet, sint tritani pri id, pro veri delenit at, ius ex nibh quaestio. At sit nemore accusata urbanitas, te decore possit pri. Eu placerat neglegentur mei, vel id dicat ancillae. Eos postea prompta id.
                                    </p>
                                </div>
                            </div>
                            <div class="col-12 col-lg-6">
                                <div class="fact">
                                    <img src="../img/megaphone-1.svg" alt="megaphone">
                                    <p>
                                        Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Phasellus enim erat, vestibulum vel, aliquam a, posuere eu, velit.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-12">
                                <h2 class="section-header bold-header">
                                    Główne fakapy reprywatyzacyjne
                                </h2>
                            </div>
                        </div>
                        <tabs>
                            <tab class="col-12" name='Parki' :selected='true'>
                                <h4 class="bold-header">
                                    Opis zjawiska
                                </h4>
                                <p>
                                    Lorem ipsum dolor sit amet, sint tritani pri id, pro veri delenit at, ius ex nibh quaestio. At sit nemore accusata urbanitas, te decore possit pri. Eu placerat neglegentur mei, vel id dicat ancillae. Eos postea prompta id. Phasellus enim erat, vestibulum vel, aliquam a, posuere eu, velit. Morbi scelerisque luctus velit. Praesent in mauris eu tortor porttitor accumsan. Mauris dolor felis, sagittis at, luctus sed, aliquam non, tellus. Proin pede metus, vulputate nec.
                                </p>
                                <h4 class="bold-header">
                                    Skala zjawiska
                                </h4>
                                <p>
                                    Lorem ipsum dolor sit amet, sint tritani pri id, pro veri delenit at, ius ex nibh quaestio. At sit nemore accusata urbanitas, te decore possit pri. Eu placerat neglegentur mei, vel id dicat ancillae. Eos postea prompta id. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Phasellus enim erat, vestibulum vel, aliquam a, posuere eu, velit.
                                </p>
                                <h4 class="bold-header">
                                    Co możemy zrobić?
                                </h4>
                                <p>
                                    Lorem ipsum dolor sit amet, sint tritani pri id, pro veri delenit at, ius ex nibh quaestio. At sit nemore accusata urbanitas, te decore possit pri. Eu placerat neglegentur mei, vel id dicat ancillae. Eos postea prompta id.
                                </p>
                            </tab>
                            <tab class="col-12" name='Szkoły'>
                                <h4 class="bold-header">
                                    Opis zjawiska
                                </h4>
                                <p>
                                    Lorem ipsum dolor sit amet, sint tritani pri id, pro veri delenit at, ius ex nibh quaestio. At sit nemore accusata urbanitas, te decore possit pri. Eu placerat neglegentur mei, vel id dicat ancillae. Eos postea prompta id. Phasellus enim erat, vestibulum vel, aliquam a, posuere eu, velit. Morbi scelerisque luctus velit. Praesent in mauris eu tortor porttitor accumsan. Mauris dolor felis, sagittis at, luctus sed, aliquam non, tellus. Proin pede metus, vulputate nec.
                                </p>
                            </tab>
                            <tab class="col-12" name='Handel roszczeniami'>
                                <h4 class="bold-header">
                                    Skala zjawiska
                                </h4>
                                <p>
                                    Lorem ipsum dolor sit amet, sint tritani pri id, pro veri delenit at, ius ex nibh quaestio. At sit nemore accusata urbanitas, te decore possit pri. Eu placerat neglegentur mei, vel id dicat ancillae. Eos postea prompta id. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Phasellus enim erat, vestibulum vel, aliquam a, posuere eu, velit.
                                </p>
                            </tab>
                            <tab class="col-12" name='Mieszkańcy na bruku'>
                                <h4 class="bold-header">
                                    Co możemy zrobić?
                                </h4>
                                <p>
                                    Lorem ipsum dolor sit amet, sint tritani pri id, pro veri delenit at, ius ex nibh quaestio. At sit nemore accusata urbanitas, te decore possit pri. Eu placerat neglegentur mei, vel id dicat ancillae. Eos postea prompta id.
                                </p>
                            </tab>
                        </tabs>
                        <div class="row">
                            <div class="bottom-el">
                                <img class="sad" src="../img/sad.svg" alt="sad">
                                <div class="copy">
                                    <h4 class="bold-header">
                                        Jesteś osobą poszkodowaną w wyniku reprywatyzacji?
                                    </h4>
                                    <p>
                                        Zobacz jak prawidłowo przeanalizować decyzję.
                                    </p>
                                </div>
                                <button type="button" class="btn btn-warning btn-lg">Sprawdź</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import Vue from 'vue'
    import Fuse from 'fuse.js'
var options = {
    shouldSort: true,
    threshold: 0.5,
    location: 0,
    distance: 100000000,
    maxPatternLength: 32,
    minMatchCharLength: 1,
    keys: [
        "title",
        "content"
    ]
};
    export default {
    data() {
        return {
            query: "",
            posts: [],
            currentPosts: [],
            searchStarted: false,
            selectedArticle: { id: -1, parentId: -1, title: 'Wstęp' }
        };
    },
    async created() {
        var result = await this.$http.get('/api/posts/GetAllPublished/');
        this.posts = result.data;
        this.posts.splice(0, 0, this.selectedArticle);
        this.currentPosts = this.posts;
    },
    methods: {
        searchKB() {
            if (this.query.length > 0) {
                this.searchStarted = true;
                var fuse = new Fuse(this.posts, options);
                this.currentPosts = fuse.search(this.query);
            }
            else {
                this.currentPosts = posts;
                this.selectedArticle = this.posts[0];
            }
        },
        selectArticle(id) {
            if (this.posts.filter(x => x.id == id).length > 0) {
                this.selectedArticle = this.posts.filter(x => x.id == id)[0];
            }
        },
        isLinkSelected(id) {
            return id == this.selectArticle.id;
        }
    }
}

Vue.component('tabs',{
  template:`
  <div class="row">
    <div class="tabs col-12 col-lg-3 order-1">
        <div class="row">
            <div class="col-12">
                <ul class="card-shadow">
                    <li v-for='tab in tabs' :class="{'is-active': tab.isActive}">
                        <a :href='tab.href' @click='selectedTab(tab)'>{{tab.name}}</a>
                    </li>
                </ul>
            </div>
        </div>
    </div>
    <div class='tab-detail py-3 px-3 col-12 col-lg-9 card-shadow order-2'>
      <slot></slot>
    </div>
  </div>
`,
  data(){
    return {
        tabs:[],
    };
  },
  created(){
      this.tabs = this.$children;
  },
  methods:{
    selectedTab(selectedTab){
      this.tabs.forEach( tab =>{
        tab.isActive = (tab.name == selectedTab.name) }
       )
    }
  }
})

Vue.component('tab',{
  template:`
    <div v-show='isActive'><slot></slot></div>
  `,
  props:{
    name:{required:true},
    selected:{default:false}
  },
  data(){
    return {
        isActive:false
      }
  },
  computed:{
     href(){
       return '#'+this.name.toLowerCase().replace(/ /g,'-');
     }
  },
  mounted(){
    this.isActive = this.selected
  }
})

</script>

<style>
    #knowledge-base{
        min-height: 150vh;
    }

    #posts-nav > div{
        cursor: pointer;
    }

    #posts-nav .link-selected{
        text-decoration: underline;
    }

    #knowledge-base .broad-view {
        height: 100%;
        margin-top: 0;
    }

    .fact {
        width: 100%;
        display: flex;
        padding: 0 20px;
    }

        .fact img {
            height: 50px;
            margin-right: 20px;
            transition: all .2s ease-in-out;
        }

        .fact:hover img {
            transform: scale(1.05);
        }
</style>
