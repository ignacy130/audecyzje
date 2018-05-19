<template>
    <nav class="navbar navbar-expand-lg bg-dark" v-bind:class="{ 'navbar-dark': isHome }">
        <a class="navbar-brand mx-auto" href="/">Społeczny Audyt Reprywatyzacji</a>
        <button class="navbar-toggler" type="button" data-toggle="collapse" data-target="#navbarText" aria-controls="navbarText" aria-expanded="false" aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse justify-content-between" id="navbarText">
            <ul class="navbar-nav">
                <li class="nav-item active mx-2">
                    <a class="nav-link" href="/">
                        Strona Główna
                        <span class="sr-only">(current)</span>
                    </a>
                </li>
                <li class="nav-item mx-2">
                    <a class="nav-link" href="/mapa">Mapa Reprywatyzacji</a>
                </li>
            </ul>
            <ul class="navbar-nav">
                <li class="nav-item mx-2">
                    <a class="nav-link" href="baza-wiedzy">Baza Wiedzy</a>
                </li>
                <li class="nav-item mx-2">
                    <a class="nav-link" href="kontakt">Kontakt</a>
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
