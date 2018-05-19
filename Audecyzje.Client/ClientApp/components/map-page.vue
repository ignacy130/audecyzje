<template>
    <div>
        <!-- Form -->
        <div id="map" class="jumbotron form px-4 mx-auto">
            <div class="row">
                <div class="col-md-4 input-card order-md-1">
                    <div class="input-group mb-3">
                        <input type="text" v-model="query" v-on:keyup.enter="search" class="form-control" placeholder="Wpisz adres warszawskiej nieruchomości" aria-label="Recipient's username"
                               aria-describedby="basic-addon2" />

                        <!-- <div class="dropdown">
                          <button class="btn btn-secondary dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" aria-haspopup="true"
                            aria-expanded="false"></button>
                          <div class="dropdown-menu" aria-labelledby="dropdownMenuButton">
                            <form class="px-4 py-3">
                              <div class="form-group">
                                <label for="exampleDropdownFormEmail1">Rok/Lata</label>
                                <input type="email" class="form-control" id="exampleDropdownFormEmail1" placeholder="1950-2017">
                              </div>
                              <div class="form-group">
                                <label for="exampleDropdownFormPassword1">Dzielnica</label>
                                <input type="password" class="form-control" id="exampleDropdownFormPassword1" placeholder="Wola">
                              </div>
                              <div class="form-check mb-3">
                                <input type="checkbox" class="form-check-input" id="dropdownCheck">
                                <label class="form-check-label" for="dropdownCheck">
                                  Zaznacz
                                </label>
                              </div>
                              <button type="submit" class="btn btn-primary">Znajdź</button>
                            </form>
                            <div class="dropdown-divider"></div>
                            <a class="dropdown-item" href="#">Lorem ipsum dolor sit amet</a>
                          </div>
                        </div> -->
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
                    <div id="decisions-list" v-if="decisions && decisions.length>0">
                        <div v-for="decision in decisions">
                            <!-- Success Card -->
                            <div class="card card-body" id="success">
                                <img class="img-fluid success-img" src="" alt="" />
                                <svg class="svg-inline--fa fa-times fa-w-12 fa-2x ml-3 mt-3" href="#" aria-hidden="true" data-fa-processed="" data-prefix="fas" data-icon="times" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384 512">
                                    <path fill="currentColor" d="M323.1 441l53.9-53.9c9.4-9.4 9.4-24.5 0-33.9L279.8 256l97.2-97.2c9.4-9.4 9.4-24.5 0-33.9L323.1 71c-9.4-9.4-24.5-9.4-33.9 0L192 168.2 94.8 71c-9.4-9.4-24.5-9.4-33.9 0L7 124.9c-9.4 9.4-9.4 24.5 0 33.9l97.2 97.2L7 353.2c-9.4 9.4-9.4 24.5 0 33.9L60.9 441c9.4 9.4 24.5 9.4 33.9 0l97.2-97.2 97.2 97.2c9.3 9.3 24.5 9.3 33.9 0z"></path>
                                </svg>
                                <!-- <i class="fas fa-times fa-2x ml-3 mt-3" href="#"></i> -->
                                <div class="address py-3">
                                    <h5 class="mt-4 ml-4">{{decision.localization}}</h5>
                                    <p class="mb-4 ml-4">Śródmieście, Warszawa</p>
                                </div>
                                <div class="card-content">
                                    <div class="row mx-auto mt-5">
                                        <div class="col-md-2">
                                            <svg class="svg-inline--fa fa-home fa-w-18 fa-2x mb-2" aria-hidden="true" data-fa-processed="" data-prefix="fas" data-icon="home" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 576 512">
                                                <path fill="currentColor" d="M488 312.7V456c0 13.3-10.7 24-24 24H348c-6.6 0-12-5.4-12-12V356c0-6.6-5.4-12-12-12h-72c-6.6 0-12 5.4-12 12v112c0 6.6-5.4 12-12 12H112c-13.3 0-24-10.7-24-24V312.7c0-3.6 1.6-7 4.4-9.3l188-154.8c4.4-3.6 10.8-3.6 15.3 0l188 154.8c2.7 2.3 4.3 5.7 4.3 9.3zm83.6-60.9L488 182.9V44.4c0-6.6-5.4-12-12-12h-56c-6.6 0-12 5.4-12 12V117l-89.5-73.7c-17.7-14.6-43.3-14.6-61 0L4.4 251.8c-5.1 4.2-5.8 11.8-1.6 16.9l25.5 31c4.2 5.1 11.8 5.8 16.9 1.6l235.2-193.7c4.4-3.6 10.8-3.6 15.3 0l235.2 193.7c5.1 4.2 12.7 3.5 16.9-1.6l25.5-31c4.2-5.2 3.4-12.7-1.7-16.9z"></path>
                                            </svg>
                                            <!-- <i class="fas fa-home fa-2x mb-2"></i> -->
                                        </div>
                                        <div class="col-md-10">
                                            <p>Liczba lokali: 15</p>
                                            <p>Liczba lokatorów na dzień 31.12.2017 r.: 43</p>
                                        </div>
                                    </div>
                                    <div class="row mx-auto mt-4">
                                        <div class="col-md-2">
                                            <svg class="svg-inline--fa fa-file-pdf fa-w-12 fa-2x mb-2 ml-1" aria-hidden="true" data-fa-processed="" data-prefix="fas" data-icon="file-pdf" role="img" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 384 512">
                                                <path fill="currentColor" d="M181.9 256.1c-5-16-4.9-46.9-2-46.9 8.4 0 7.6 36.9 2 46.9zm-1.7 47.2c-7.7 20.2-17.3 43.3-28.4 62.7 18.3-7 39-17.2 62.9-21.9-12.7-9.6-24.9-23.4-34.5-40.8zM86.1 428.1c0 .8 13.2-5.4 34.9-40.2-6.7 6.3-29.1 24.5-34.9 40.2zM248 160h136v328c0 13.3-10.7 24-24 24H24c-13.3 0-24-10.7-24-24V24C0 10.7 10.7 0 24 0h200v136c0 13.2 10.8 24 24 24zm-8 171.8c-20-12.2-33.3-29-42.7-53.8 4.5-18.5 11.6-46.6 6.2-64.2-4.7-29.4-42.4-26.5-47.8-6.8-5 18.3-.4 44.1 8.1 77-11.6 27.6-28.7 64.6-40.8 85.8-.1 0-.1.1-.2.1-27.1 13.9-73.6 44.5-54.5 68 5.6 6.9 16 10 21.5 10 17.9 0 35.7-18 61.1-61.8 25.8-8.5 54.1-19.1 79-23.2 21.7 11.8 47.1 19.5 64 19.5 29.2 0 31.2-32 19.7-43.4-13.9-13.6-54.3-9.7-73.6-7.2zM377 105L279 7c-4.5-4.5-10.6-7-17-7h-6v128h128v-6.1c0-6.3-2.5-12.4-7-16.9zm-74.1 255.3c4.1-2.7-2.5-11.9-42.8-9 37.1 15.8 42.8 9 42.8 9z"></path>
                                            </svg>
                                            <!-- <i class="fas fa-file-pdf fa-2x mb-2 ml-1"></i> -->
                                        </div>
                                        <div class="col-md-10">
                                            <p>Decyzja nr: {{decision.decisionNumber}}</p>
                                            <p>Wydana {{decision.date}} r. przez Urząd Miasta Stołecznego Warszawa</p>
                                            <p>Rodzaj nieruchomości: xxx</p>
                                        </div>
                                    </div>
                                </div>
                            </div>

                        </div>

                        <!-- Error Card -->
                        <div class="card card-body hide" id="error">
                            <img class="img-fluid mb-4" src="http://lorempixel.com/400/200" alt="" />
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
                            <img class="img-fluid mb-4" src="http://lorempixel.com/400/200" alt="" />
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
                    <div v-if="searchPerformed && decisions && decisions.length <= 0">
                        Brak wyników
                    </div>

                </div>
                <div class="col-md-8 map-responsive mb-5 order-md-12">
                    <l-map :zoom="zoom" :center="center" style="height: 1000px; width: 1000px;">
                        <v-geosearch :options="geosearchOptions"></v-geosearch>
                        <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>
                        <div v-for="marker in markers">
                            <marker-popup :position="marker.position" :text="marker.text" :title="marker.title"></marker-popup>
                        </div>
                    </l-map>
                </div>
            </div>
        </div>
    </div>
</template>


<script>
    import Vue from 'vue'
    import { LMap, LTileLayer } from 'vue2-leaflet';
    import { OpenStreetMapProvider } from 'leaflet-geosearch';
    import { VGeosearch } from 'vue2-leaflet-geosearch';
    import MarkerPopup from './map-popup';

    const provider = new OpenStreetMapProvider();

    export default {
        components: {
            LMap,
            LTileLayer,
            VGeosearch,
            MarkerPopup
        },
        data() {
            return {
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
            }
        },
        methods: {
            search: async function (event) {
                event.preventDefault();
                var searchQuery = this.query;
                this.searching = true;
                this.searchPerformed = false;
                try {
                    let response = await this.$http.get('/api/document/search/?query=' + encodeURIComponent(searchQuery))
                    this.decisions = response.data;

                    var m = [];
                    for (var i = 0; i < this.decisions.length; i++) {
                        var results = await this.$http.get('https://nominatim.openstreetmap.org/search?format=json&q=' + encodeURIComponent(this.decisions[i].localization + ", Warszawa, Polska"));
                        var first = results.data[0];
                        m.push({ position: L.latLng(first.lat, first.lon), text: this.decisions[i].decisionNumber, title: this.decisions[i].decisionNumber });
                    }
                    this.markers = m;
                } catch (error) {
                    console.log(error)
                }
                this.searchPerformed = true;
            },
        },
    }
</script>

<style>
    @import "~leaflet/dist/leaflet.css";
</style>
