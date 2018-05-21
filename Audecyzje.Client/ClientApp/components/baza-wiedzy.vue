<template>
    <div class="subpage-view">
        <div class="view">
            <div class="container pt-5">
                <div class="text-center input-field py-5">
                    <h1 class="bold-header text-center">Dostęp do ponad 2000 decyzji reprywatyzacyjnych wydanych w Warszawie w latach 1950-2017</h1>
                    <div class="input-group my-4">
                        <input type="text" class="form-control" aria-label="Text input with dropdown button" placeholder="Wpisz adres warszawskiej nieruchomości" />
                        <button type="button" class="btn btn-warning btn-lg">Sprawdź</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="container">
            <div class="row">
                <div class="definition col-12 col-lg-7">
                    <h2 class="bold-header">
                        Czym jest reprywatyzacja
                    </h2>
                    <p>
                        Lorem ipsum dolor sit amet, sint tritani pri id, pro veri delenit at, ius ex nibh quaestio. At sit nemore accusata urbanitas, te decore possit pri. Eu placerat neglegentur mei, vel id dicat ancillae. Eos postea prompta id.
                    </p>
                    <div class="quote">
                        <img src="../img/quote-1.svg" alt="quote">
                        <p class="quote-text-small">
                            Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Phasellus enim erat, vestibulum vel, aliquam a, posuere eu, velit. Morbi scelerisque luctus velit. Praesent in mauris eu tortor porttitor accumsan. Mauris dolor felis, sagittis at, luctus sed, aliquam non, tellus. Proin pede metus, vulputate nec, fermentum fringilla, vehicula vitae, justo. In enim a arcu imperdiet malesuada. Vivamus luctus egestas leo.
                        </p>
                    </div>
                </div>
                <div class="side-image col-5"></div>
            </div>
            <div class="row">
                <div class="col-12">
                    <h2 class="section-header bold-header">
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
  <div class="row no-gutters">
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
