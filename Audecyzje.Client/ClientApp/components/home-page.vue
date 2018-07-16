<template>
    <div>
        <!-- Input Field -->
        <div class="broad-view">
            <div class="container pt-5">
                <div class="text-center input-field py-4">
                    <h1 class="bold-header text-center px-lg-5">Dostęp do ponad 2000 decyzji reprywatyzacyjnych wydanych w Warszawie w latach 1950-2017</h1>
                    <div class="input-group my-4 my-lg-5">
                        <input type="text" class="form-control" aria-label="Text input with dropdown button" placeholder="Wpisz adres warszawskiej nieruchomości"/>
                            <button type="button" class="btn btn-warning btn-lg">Sprawdź</button>
                        </div>
                    <h5 class="text-center px-lg-5">
                        Lorem Lorem ipsum dolor sit amet augue. Praesent gravida pulvinar. Nulla facilisi. Etiam vel nulla. Curabitur scelerisque
                        tellus augue, ullamcorper ut, condimentum urna, placerat tempor ac, suscipit in, vulputate tortor.
                    </h5>
                </div>
            </div>
        </div>

        <!-- Stats -->
        <section>
            <div class="container">
                <div class="stats">
                    <div class="card-columns">
                        <div class="card">
                            <div class="card-body">
                                <h1 class="card-title text-center">20 tys.</h1>
                                <div>
                                    <p class="card-text">Aż tyle osób straciło dach nad głową w wyniku reprywatyzacji.</p>
                                    <p>To 1% mieszkańców Warszawy.</p>
                                </div>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-body">
                                <h1 class="card-title text-center">450</h1>
                                <div>
                                    <p class="card-text">Tyle nieruchomości zostało przekazanych w latach 1950-2017.</p>
                                    <p>To 3% budynkow w Warszawie.</p>
                                </div>
                            </div>
                        </div>
                        <div class="card">
                            <div class="card-body">
                                <h1 class="card-title text-center">15 mln</h1>
                                <div>
                                    <p class="card-text">To łączna wartość zreprywatyzowanych nieruchomości w stolicy.</p>
                                    <p>To 5% budżetu rocznego Warszawy.</p>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </section>


        <!-- Info -->
        <section class="container info my-5">
            <div class="row">
                <div class="col-md-3 col-sm-6 col-12 mb-4 d-flex flex-column">
                    <h5 class="font-weight-bold">Reprywatyzacja w Twojej dzielnicy</h5>
                    <p>Lorem ipsum dolor sit amet augue. Praesent gravida pulvinar. Nulla facilisi. Etiam vel nulla.</p>
                    <router-link class="mt-auto" to="lokatorzy">Przeglądaj ></router-link>
                </div>
                <div class="col-md-3 col-sm-6 col-12 mb-4 d-flex flex-column">
                    <h5 class="font-weight-bold">Raporty i opracowania</h5>
                    <p>Lorem ipsum dolor sit amet augue. Praesent gravida pulvinar. Nulla facilisi. Etiam vel nulla.</p>
                   <router-link class="mt-auto" to="decyzja">Przeglądaj ></router-link>
                </div>
                <div class="col-md-3 col-sm-6 col-12 mb-4 d-flex flex-column">
                    <h5 class="font-weight-bold">Baza wiedzy o reprywatyzacji</h5>
                    <p>Lorem ipsum dolor sit amet augue. Praesent gravida pulvinar. Nulla facilisi. Etiam vel nulla.</p>
                    <router-link class="mt-auto" to="baza-wiedzy">Przeglądaj ></router-link>
                </div>
                <div class="col-md-3 col-sm-6 col-12 mb-4 d-flex flex-column">
                    <h5 class="font-weight-bold">Kontakt/ źródła opracowań</h5>
                    <p>Lorem ipsum dolor sit amet augue. Praesent gravida pulvinar. Nulla facilisi. Etiam vel nulla.</p>
                    <router-link class="mt-auto" to="baza-wiedzy">Przeglądaj ></router-link>
                </div>
            </div>
        </section>
    </div>
</template>

<script>
import { routes } from '../routes'

export default {
    data() {
        return {
            
        }
    },
}
</script>

<style>
</style>
