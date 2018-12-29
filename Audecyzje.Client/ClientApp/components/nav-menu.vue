<template>
    <nav class="navbar navbar-expand-lg" v-bind:class="{ 'navbar-dark': isHome(), 'navbar-light': !isHome(), 'navbar-black-bg': isHome() && showMobileMenu, 'navbar-transparent': isHome() && !showMobileMenu}">
        <router-link class="navbar-brand mx-auto" to="/">
            <strong>Społeczny Audyt Reprywatyzacji</strong>
        </router-link>
        <button v-on:click="toggleNavbar" class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarText" aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="navbar-collapse collapse justify-content-between" v-bind:class="{ 'show': showMobileMenu }" id="navbarText">
            <ul class="navbar-nav">
                <li class="nav-item active mx-2">
                    <router-link class="nav-link" to="/">
                        Strona główna
                        <span class="sr-only">(current)</span>
                        <span class="line" v-bind:class="{ 'white-line' : isHome() }"></span>
                    </router-link>
                </li>
                <li class="nav-item mx-2">
                    <router-link class="nav-link" to="/mapa">
                        Mapa Reprywatyzacji
                        <span class="line" v-bind:class="{ 'white-line' : isHome() }"></span>
                    </router-link>
                </li>
            </ul>
            <ul class="navbar-nav">
                <li class="nav-item mx-2">
                    <router-link class="nav-link" to="/baza-wiedzy">
                        Baza Wiedzy
                        <span class="line" v-bind:class="{ 'white-line' : isHome() }"></span>
                    </router-link>
                </li>
                <li class="nav-item mx-2">
                    <router-link class="nav-link" to="/kontakt">
                        Kontakt
                        <span class="line" v-bind:class="{ 'white-line' : isHome() }"></span>
                    </router-link>
                </li>
            </ul>
        </div>
    </nav>
</template>
<script>
    import { routes } from '../routes'

    export default {
        data() {
            return {
                routes,
                showMobileMenu: false
            }
        },
        methods: {
            toggleNavbar: function (event) {
                this.showMobileMenu = !this.showMobileMenu;
            },
            isHome: function () {
                return this.$route.path === "/";
            },
            onResize(event) {
                if (document.body.clientWidth >= 992 && this.showMobileMenu) {
                    this.showMobileMenu = false;
                }
            }
        },
        mounted() {
            window.addEventListener('resize', this.onResize)
        },

        beforeDestroy() {
            window.removeEventListener('resize', this.onResize)
        }
    }
</script>
<style>
    .slide-enter-active, .slide-leave-active {
        transition: max-height .35s
    }

    .slide-enter, .slide-leave-to {
        max-height: 0px;
    }

    .slide-enter-to, .slide-leave {
        max-height: 20em;
    }
</style>
