<template>
    <footer class="container pt-5 pb-3" v-if="['map'].indexOf($route.name) === -1">
        <div class="row">
            <div class="col-12 col-md-3 align-self-center">
                Copyright &copy; 2017<br />
                <a href="/privacy-policy">Polityka Prywatności</a><br />
                <a href="/termsofuse">Regulamin</a>
            </div>
            <div class="col-12 col-md-9">
                <div class="row">
                    <div class="col-12 col-md-4 mt-3 align-self-center float-right ml-auto">
                        <img class="img-fluid" src="../img/kdp.png" alt="" />
                    </div>
                    <div class="col-12 col-md-4 mt-3 align-self-center float-right ml-auto">
                        <img class="img-fluid" src="../img/mjn.png" alt="" />
                    </div>
                    <div class="col-12 col-md-4 mt-3 align-self-center float-right ml-auto">
                        <img class="img-fluid" src="../img/neatcode_logo_typo_transparent_50.png" alt="" />
                    </div>
                </div>
            </div>
        </div>
    </footer>
</template>
<script>
    export default {
        data() {
            return {};
        },
        methods: {
        },
    }</script>
<style>
</style>
