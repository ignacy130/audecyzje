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
    import Map from './map'
    import HomePage from './home-page'
    import NavMenu from './nav-menu'
    import Footer from './footer-view'

    Vue.component('counter-example', CounterExample);
    Vue.component('map', Map);
    Vue.component('home-page', HomePage);
    Vue.component('nav-menu', NavMenu);
    Vue.component('footer-view', Footer);

    export default {
    data() {
        return {
            
        }
    }
    }
</script>

<style>
</style>
