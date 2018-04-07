<template>
    <div>
        <!-- Form -->
        <div class="jumbotron form px-4 mx-auto">
            <div class="row">
                <div class="col-md-4">
                    <div class="input-group mb-3">
                        <input type="text" class="form-control" placeholder="Wpisz adres warszawskiej nieruchomości" aria-label="Recipient's username"
                          aria-describedby="basic-addon2"/>
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
                                <button class="btn btn-primary" type="button" data-toggle="collapse" data-target="#collapseExample" aria-expanded="false"
                                  aria-controls="collapseExample">Szukaj</button>
                            </div>
                            
                        </div>
                    <div class="" v-if="forecasts">
                        <div v-for="decision in forecasts">
                            <!-- Success Card -->
                            <div class="card card-body" id="success">
                                <img class="img-fluid mb-4" src="" alt=""/>
                                <div class="row">
                                    <div class="col-md-2">
                                        <i class="fas fa-file-pdf fa-lg"></i>
                                    </div>
                                    <div class="col-md-10">
                                        <p>Decyzja nr {{decision.decisionNumber}}</p>
                                        <p>Wydana {{decision.date}}</p>
                                        <p>przez xxxx</p>
                                        <p>Rodzaj nieruchomości: xxxx</p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-2">
                                        <i class="fas fa-home fa-lg"></i>
                                    </div>
                                    <div class="col-md-10">
                                        <p>Liczba lokali: xxxx</p>
                                        <p>Liczba lokatorów na dzień x: xxx</p>
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
                                <!-- <div class="row">
                <div class="col-md-2">
                  <i class="fas fa-comment-alt fa-lg"></i>
                </div>
                <div class="col-md-10">
                  <p>Komentarze</p>
                  <p class="font-weight-bold">Janusz Komentatorski</p>
                  <p class="text-justify">Ambitioni dedisse scripsisse iudicaretur. Cras mattis iudicium purus sit amet fermentum. Donec sed odio
                    operae, eu vulputate felis rhoncus. Praeterea iter est quasdam res quas ex communi. At nos hinc posthac,
                    sitientis piros Afros. Petierunt uti sibi concilium totius Galliae in diem certam indicere. Cras mattis
                    iudicium purus sit amet fermentum.
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
              </div> -->
                            </div>
                        </div>
                        <!-- Error Card -->
                        <div class="card card-body hide" id="error">
                            <img class="img-fluid mb-4" src="http://lorempixel.com/400/200" alt=""/>
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
                            <img class="img-fluid mb-4" src="http://lorempixel.com/400/200" alt=""/>
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
                </div>
                <div class="col-md-8 map-responsive">
                    <iframe src="https://www.google.com/maps/embed?pb=!1m14!1m12!1m3!1d9775.077946251844!2d21.01258967137917!3d52.22940567198604!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!5e0!3m2!1spl!2spl!4v1517337615480"
                      width="600" height="450" frameborder="0" style="border:0" allowfullscreen=""></iframe>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    data() {
        return {
            forecasts: null
        }
    },

    methods: {
    },

    async created() {
        // ES2017 async/await syntax via babel-plugin-transform-async-to-generator
        // TypeScript can also transpile async/await down to ES5
        try {
            let response = await this.$http.get('/api/document/getbydecisionnumber/1234')
            console.log(response.data);
            this.forecasts = response.data;
        } catch (error) {
            console.log(error)
        }
        // Old promise-based approach
        //this.$http
        //    .get('/api/SampleData/WeatherForecasts')
        //    .then(response => {
        //        console.log(response.data)
        //        this.forecasts = response.data
        //    })
        //    .catch((error) => console.log(error))*/
    }
}
</script>

<style>
</style>
