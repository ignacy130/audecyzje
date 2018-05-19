<template>
    <div id='app'>
        <nav-menu />
        <router-view></router-view>
        <footer-view />
    </div>
</template>

<script>
    import Vue from 'vue'
    import CounterExample from './counter-example'
    import Map from './map-page'
    import HomePage from './home-page'
    import NavMenu from './nav-menu'
    import MapComponent from './map-component';
    import BazaWiedzy from './baza-wiedzy';
    import Footer from './footer-view'

    Vue.component('counter-example', CounterExample);
    Vue.component('map-page', Map);
    Vue.component('home-page', HomePage);
    Vue.component('nav-menu', NavMenu);
    Vue.component('map-component', MapComponent);
    Vue.component('baza-wiedzy', BazaWiedzy);
    Vue.component('footer-view', Footer);

    export default {
    data() {
        return {

        }
    }
    }</script>

<style>
</style>
