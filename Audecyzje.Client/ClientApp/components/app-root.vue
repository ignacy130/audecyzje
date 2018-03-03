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
    import FetchData from './fetch-data'
    import HomePage from './home-page'
    import NavMenu from './nav-menu'
    import Footer from './footer-view'

    Vue.component('counter-example', CounterExample);
    Vue.component('fetch-data', FetchData);
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
