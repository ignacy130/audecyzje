<template>
    <nav class="navbar navbar-expand-lg bg-dark" v-bind:class="{ 'navbar-dark': isHome }">
        <router-link class="navbar-brand mx-auto" to="/">Społeczny Audyt Reprywatyzacji</router-link>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarText" aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse justify-content-between" id="navbarText">
            <ul class="navbar-nav">
                <li class="nav-item active mx-2">
                    <router-link class="nav-link" to="/">
                    Strona główna
                        <span class="sr-only">(current)</span>
                    </router-link>
                </li>
                <li class="nav-item mx-2">
                    <router-link class="nav-link" to="mapa">Mapa Reprywatyzacji</router-link>
                </li>
            </ul>
            <ul class="navbar-nav">
                <li class="nav-item mx-2">
                    <router-link class="nav-link" to="baza-wiedzy">Baza Wiedzy</router-link>
                </li>
                <li class="nav-item mx-2">
                    <router-link class="nav-link" to="kontakt">Kontakt</router-link>
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
                isHome: this.$route.path === "/",
                routes,
                collapsed: true
            }
        },
        methods: {
            toggleCollapsed: function (event) {
                this.collapsed = !this.collapsed;
            }
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
