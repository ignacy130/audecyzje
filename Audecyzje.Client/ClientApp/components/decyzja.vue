<template>
    <div id="decisions" class="subpage-view">
        <div class="broad-view">
            <div class="row mx-4 mx-lg-5 mt-5">
                <div class="definition col-12 col-lg-10 col-xl-7 offset-xl-1 py-5 pl-0">
                    <h2 class="bold-header">
                        Jak czytać decyzję
                    </h2>
                    <p class="copy">
                        Lorem ipsum dolor sit amet, sint tritani pri id, pro veri delenit at, ius ex nibh quaestio. At sit nemore accusata urbanitas, te decore possit pri. Eu placerat neglegentur mei, vel id dicat ancillae. Eos postea prompta id.
                    </p>
                    <div class="decision-steps">
                        <div class="decision-step">
                            <div class="step-nr">
                                <span>
                                    1
                                </span>
                            </div>
                            <p>Znajdź decyzję i zapisz ją na dysku</p>
                        </div>
                        <div class="decision-step">
                            <div class="step-nr">
                                <span>
                                    2
                                </span>
                            </div>
                            <p>Przejrzyj kroki analizy</p>
                        </div>
                        <div class="decision-step">
                            <div class="step-nr">
                                <span>
                                    3
                                </span>
                            </div>
                            <p>Znajdź nieprawidłowości w decyzji</p>
                        </div>
                        <button type="button" class="btn btn-warning btn-lg mt-5">
                            Wyszukaj decyzję
                        </button>
                     </div>
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row">
                 <div class="col-12">
                    <h2 class="section-header">
                        Sprawdź czy decyzja jest prawidłowa
                    </h2>  
                </div>
                <div class="col-12 col-lg-6">
                    <p>
                        Lorem ipsum dolor sit amet enim. Etiam ullamcorper. Suspendisse a pellentesque dui, non felis. Maecenas malesuada elit lectus felis, malesuada ultricies. Curabitur et ligula. Ut molestie a, ultricies porta urna. Vestibulum commodo volutpat a, convallis ac.
                    </p>  
                </div>
            </div>
            <tabs>
                <tab class="col-12" name='Jaką decyzję sprawdzasz?' :selected='true'>
                    <h4 class="bold-header">
                        Jaką decyzję sprawdzasz?
                    </h4>
                    <p>
                        Lorem ipsum dolor sit amet, sint tritani pri id, pro veri delenit at, ius ex nibh quaestio. At sit nemore accusata urbanitas, te decore possit pri. Eu placerat neglegentur mei, vel id dicat ancillae. Eos postea prompta id.
                    </p>
                </tab>
                <tab class="col-12" name='Czy wystąpił kurator?'>
                    <h4 class="bold-header">
                        Czy wystąpił kurator?
                    </h4>
                    <p>
                        Lorem ipsum dolor sit amet, sint tritani pri id, pro veri delenit at, ius ex nibh quaestio. At sit nemore accusata urbanitas, te decore possit pri. Eu placerat neglegentur mei, vel id dicat ancillae. Eos postea prompta id. Phasellus enim erat, vestibulum vel, aliquam a, posuere eu, velit. Morbi scelerisque luctus velit.
                    </p>
                </tab>
                <tab class="col-12" name='Czy byli mieszkańcy?'>
                    <h4 class="bold-header">
                        Czy byli mieszkańcy?
                    </h4>
                    <p>
                        Lorem ipsum dolor sit amet, sint tritani pri id, pro veri delenit at, ius ex nibh quaestio. At sit nemore accusata urbanitas, te decore possit pri. Eu placerat neglegentur mei, vel id dicat ancillae. Eos postea prompta id. Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Phasellus enim erat, vestibulum vel, aliquam a, posuere eu, velit.
                    </p>
                </tab>
                <tab class="col-12" name='Podsumowanie'>
                    <h4 class="bold-header">
                        Podsumowanie
                    </h4>
                    <p>
                        Lorem ipsum dolor sit amet, sint tritani pri id, pro veri delenit at, ius ex nibh quaestio. At sit nemore accusata urbanitas, te decore possit pri. Eu placerat neglegentur mei, vel id dicat ancillae. Eos postea prompta id.
                    </p>
                </tab>
            </tabs>
            <div class="row">
                <div class="bottom-el">
                     <img class="file" src="../img/file.svg" alt="file">
                     <div class="copy">
                        <h4>
                            Chwytliwe podsumowanie raportu
                        </h4>
                        <p>
                            Poznaj wnioski z audytu reprywatyzacji
                        </p>
                     </div>
                     <button type="button" class="btn btn-warning btn-lg">
                         Pobierz raport
                     </button>
                </div>
            </div> 
             <div class="row">
                <div class="bottom-el">
                     <img class="sad" src="../img/sad.svg" alt="sad">
                     <div class="copy">
                        <h4>
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
</template>
<script>
import Vue from 'vue'

export default {
    data() {
        return {

        }
    }
}

Vue.component('tabs',{
  template:`
  <div class="row mt-5">
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
        tabs:[]
    };
  },
  created(){
    this.tabs = this.$children
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
    #decisions .broad-view {
        height: 100%;
        margin-top: 0;
    }

    #decisions .tabs ul li a {
        font-size: 1rem;
    }

    .decision-step {
        display: flex;
        align-items: center;
    }

    .decision-step .step-nr {
        position: relative;
        height: 45px;
        width: 45px;
        border: 1px solid #fff;
        border-radius: 100px;
        margin: 10px 20px 10px 0;
        display: flex;
        justify-content: center;
        align-items: center;
        transition: all .2s ease-in-out;
    }

    .decision-step:hover .step-nr {
        transform: scale(1.1);
    }

    .decision-step .step-nr span {
        font-size: 1.1rem;
        font-weight: 600;
    }

    .decision-step p {
        margin-bottom: 0;
    }
</style>