<template>
    <div>
        <!-- Form -->
        <div id="map" class="">
            <div class="row mr-0">
                <div class="d-inline-block col-md-4 pr-0">
                    <div class="input-card mx-0 my-0 py-0 d-flex flex-column order-md-1">
                        <div class="input-group">
                            <vue-simple-suggest :list="suggestions"
                                                :filter-by-query="true"
                                                ref="queryInput"
                                                v-model="query"
                                                @hover="onSuggestHover"
                                                @select="search"
                                                v-on:suggestion-click="search"
                                                class="form-control"
                                                :styles="suggestStyles"
                                                placeholder="Wpisz adres warszawskiej nieruchomości">
                            </vue-simple-suggest>

                            <div class="input-group-append">
                                <button v-on:click="search" class="btn" type="button" data-toggle="collapse" data-target="#collapseExample" aria-expanded="false"
                                        aria-controls="collapseExample">
                                    <i class="fas fa-search"></i>
                                </button>
                            </div>

                        </div>
                        <div v-if="searching && !searchPerformed" class="text-center py-2 px-2">
                            <i class="far fa-file-alt fa-spin fa-2x"></i>
                        </div>
                        <div class="mt-3 ml-2" id="decisions-list-container">
                            <div id="decisions-list" v-if="showDecisions && (decisions && decisions.length>0)">
                                <div v-for="decision in decisions">
                                    <!-- Success Card -->
                                    <div class="card card-body mb-3 p-0 ml-1 mr-3">
                                        <img class="img-fluid success-img" src="" alt="" />
                                        <!--<i class="fas fa-times fa-2x ml-3 mt-3" href="#"></i>-->
                                        <div class="address py-3">
                                            <h5 class="mt-2 ml-4">{{decision.address}}</h5>
                                            <p class="mb-2 ml-4">Warszawa</p>
                                        </div>
                                        <div class="card-content mt-3 mb-4">
                                            <div class="row mx-auto">
                                                <div class="col-md-2">
                                                    <i class="fas fa-home fa-2x mb-2"></i>
                                                </div>
                                                <div class="col-md-10">
                                                    <p>Liczba lokali: 15</p>
                                                    <p>Liczba lokatorów na dzień 31.12.2017 r.: 43</p>
                                                </div>
                                            </div>
                                            <div class="row mx-auto mt-4">
                                                <div class="col-md-2">
                                                    <a :href="decision.sourceLink" target="_blank" title="Zobacz dokument w BIP">
                                                        <i class="fas fa-file-pdf fa-2x mb-2 ml-1"></i>
                                                    </a>
                                                </div>
                                                <div class="col-md-10">
                                                    <p>Decyzja nr: <strong> {{decision.decisionNumber}}</strong></p>
                                                    <p>Wydana: {{decision.date}} r.</p>
                                                    <small>
                                                        <a :href="decision.sourceLink" target="_blank">
                                                            Źródło: BIP
                                                        </a>
                                                    </small>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>

                                <!-- Error Card -->
                                <div class="card card-body hide" id="error">
                                    <img class="img-fluid mb-4" src="" alt="" />
                                    <div class="row">
                                        <div class="col-md-2">
                                            <i class="fas fa-frown fa-lg"></i>
                                        </div>
                                        <div class="col-md-10">
                                            <p class="text-justify">Nie mamy żadnej decyzji dotyczącej tego adresu.</p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <i class="fab fa-wpforms fa-lg"></i>
                                        </div>
                                        <div class="col-md-10">
                                            <p class="text-justify">
                                                Jeśli chcesz złożyć wniosek do xxxx skorzystaj z szablonu i wyślij go do ...
                                            </p>
                                            <button type="button" class="btn btn-primary btn-lg btn-block mb-4">Uzupełnij formularz</button>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col">
                                            <ul class="list-inline">
                                                <li class="list-inline-item">
                                                    <i class="fas fa-plus-circle fa-lg color-blue"></i>
                                                </li>
                                                <li class="list-inline-item">
                                                    <p>Dodaj komentarz</p>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col">
                                            <ul class="list-inline">
                                                <li class="list-inline-item">
                                                    <i class="fas fa-users fa-lg color-blue"></i>
                                                </li>
                                                <li class="list-inline-item">
                                                    <p>Historie lokatorów</p>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-6">
                                            <ul class="list-inline">
                                                <li class="list-inline-item">
                                                    <i class="fas fa-file-alt fa-lg color-blue"></i>
                                                </li>
                                                <li class="list-inline-item">
                                                    <p>Zobacz artykuły</p>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>

                                <!-- District Card -->
                                <div class="card card-body hide" id="district">
                                    <img class="img-fluid mb-4" src="" alt="" />
                                    <div class="row">
                                        <div class="col-md-2">
                                            <i class="fas fa-file-pdf fa-lg"></i>
                                        </div>
                                        <div class="col-md-10">
                                            <p>
                                                X wydanych decyzji
                                                <a href="">Pokaż</a>
                                            </p>
                                            <p>W latach:</p>
                                            <div class="btn-group">
                                                <button class="btn btn-secondary btn-sm dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    1950
                                                </button>
                                                <div class="dropdown-menu">
                                                    ...
                                                </div>
                                            </div>
                                            <div class="btn-group">
                                                <button class="btn btn-secondary btn-sm dropdown-toggle" type="button" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                                    2017
                                                </button>
                                                <div class="dropdown-menu">
                                                    ...
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-2">
                                            <i class="fas fa-home fa-lg"></i>
                                        </div>
                                        <div class="col-md-10">
                                            <p>Ilość lokali: xxxx</p>
                                            <p>Ilość lokatorów na dzień x: xxx</p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-md-2">
                                            <i class="fas fa-exclamation-circle fa-lg"></i>
                                        </div>
                                        <div class="col-md-10">
                                            <p>Zwróć uwagę na:</p>
                                            <p>
                                                Tag 1
                                                <a href="url">co to znaczy?</a>
                                            </p>
                                            <p>
                                                Tag 2
                                                <a href="url">co to znaczy?</a>
                                            </p>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col">
                                            <ul class="list-inline">
                                                <li class="list-inline-item">
                                                    <i class="fas fa-plus-circle fa-lg color-blue"></i>
                                                </li>
                                                <li class="list-inline-item">
                                                    <p>Dodaj komentarz</p>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="col">
                                            <ul class="list-inline">
                                                <li class="list-inline-item">
                                                    <i class="fas fa-users fa-lg color-blue"></i>
                                                </li>
                                                <li class="list-inline-item">
                                                    <p>Historie lokatorów</p>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-6">
                                            <ul class="list-inline">
                                                <li class="list-inline-item">
                                                    <i class="fas fa-file-alt fa-lg color-blue"></i>
                                                </li>
                                                <li class="list-inline-item">
                                                    <p>Zobacz artykuły</p>
                                                </li>
                                            </ul>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="ml-1" v-if="searchPerformed && decisions && decisions.length <= 0">
                                Brak wyników
                            </div>
                        </div>
                    </div>
                </div>
                <l-map :zoom="zoom" :center="center" class="d-inline-block col-md-8 ml-0" style="height: 93vh;">
                    <v-geosearch :options="geosearchOptions"></v-geosearch>
                    <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>
                    <div v-for="marker in markers">
                        <marker-popup :position="marker.position" :text="marker.text" :title="marker.title"></marker-popup>
                    </div>
                </l-map>

            </div>
        </div>
    </div>
</template>


<script>
    import Vue from 'vue'
    import { LMap, LTileLayer, LMarker } from 'vue2-leaflet';
    import 'leaflet/dist/leaflet.css'
    import { OpenStreetMapProvider } from 'leaflet-geosearch';
    import VGeosearch from 'vue2-leaflet-geosearch';
    import MarkerPopup from './map-popup';
    import streets from '../resources/streets';
    import VueSimpleSuggest from 'vue-simple-suggest';
    import 'vue-simple-suggest/dist/styles.css';

    Vue.component('l-marker', LMarker);

    delete L.Icon.Default.prototype._getIconUrl;

    L.Icon.Default.mergeOptions({
        iconRetinaUrl: require('leaflet/dist/images/marker-icon-2x.png'),
        iconUrl: require('leaflet/dist/images/marker-icon.png'),
        shadowUrl: require('leaflet/dist/images/marker-shadow.png')
    });

    const provider = new OpenStreetMapProvider();

    export default {
        components: {
            LMap,
            LTileLayer,
            VGeosearch,
            MarkerPopup,
            VueSimpleSuggest
        },
        data() {
            return {
                streetSearched: "",
                zoom: 13,
                center: [52.2297, 21.0122],
                url: 'https://tiles.wmflabs.org/bw-mapnik/{z}/{x}/{y}.png',
                attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
                markers: [],
                geosearchOptions: {
                    provider: provider,
                },
                decisions: [],
                query: "",
                searchPerformed: false,
                searching: false,
                suggestions: streets,
                suggestStyles: {
                    vueSimpleSuggest: "suggest-width",
                },
                showMap: true,
                showDecisions: true,
                windowWidth: 0,
                windowHeight: 0,
                responsive: false
            }
        },
        methods: {
            doShowMap() {
                this.showMap = true;
                if (this.responsive) {
                    this.showDecisions = false;
                }
            },
            doShowDecisions() {
                this.showDecisions = true;
                if (this.responsive) {
                    this.showMap = false;
                }
            },
            onSuggestHover: function () {
                this.query = this.$refs.queryInput.hovered;
            },
            search: async function () {
                var searchQuery = this.query;
                this.searching = true;
                this.searchPerformed = false;
                if (this.query.length > 3) {
                    try {
                        let response = await this.$http.get('/api/decisions/search/?query=' + encodeURIComponent(searchQuery))
                        this.decisions = response.data;

                        var m = [];
                        for (var i = 0; i < this.decisions.length; i++) {
                            var results = await this.$http.get('https://nominatim.openstreetmap.org/search?format=json&q=' + encodeURIComponent(this.decisions[i].address + ", Warszawa, Polska"));
                            var first = results.data[0];
                            if (first) {
                                m.push({ position: L.latLng(first.lat, first.lon), text: this.decisions[i].decisionNumber, title: this.decisions[i].decisionNumber });
                            }
                        }
                        this.markers = m;
                        if (m && m.length > 0) {
                            this.center = [m[0].position.lat, m[0].position.lng];
                        }
                    } catch (error) {
                        console.log(error)
                    }
                }
                else {
                    this.decisions = [];
                    this.markers = [];
                }
                this.searchPerformed = true;
            },
            getWindowWidth(event) {
                this.windowWidth = document.documentElement.clientWidth;
                this.responsive = this.windowWidth < 600;
                if (!this.responsive) {
                    this.showMap = true;
                    this.showDecisions = true;
                }
                else if (this.showDecisions && this.showMap) {
                    this.doShowMap()
                }
            },
            getWindowHeight(event) {
                this.windowHeight = document.documentElement.clientHeight;
            }
        },
        created() {
            this.query = this.$route.params.query;
            if (this.query && this.query.length > 3) {
                this.search(this.query);
            }
        },
        mounted() {
            this.$nextTick(function () {
                window.addEventListener('resize', this.getWindowWidth);
                window.addEventListener('resize', this.getWindowHeight);
                this.getWindowWidth()
                this.getWindowHeight()
            })
        },
        beforeDestroy() {
            window.removeEventListener('resize', this.getWindowWidth);
            window.removeEventListener('resize', this.getWindowHeight);
        }
    }
</script>

<style>
    @import "~leaflet/dist/leaflet.css";

    .input-card .input-group {
        border: none;
    }
</style>
