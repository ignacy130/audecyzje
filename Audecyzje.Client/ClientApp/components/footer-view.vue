<template>
    <footer class="container pt-5 pb-3" v-bind:class="{ 'hide': isMap }">
        <div class="row">
            <p class="col-sm-2">
                Copyright &copy; 2017
            </p>
            <p class="col-sm-2">
                <a href="/privacy-policy">Polityka Prywatności</a>
            </p>
            <p class="col-sm-1">
                <a href="/termsofuse">Regulamin</a>
            </p>
            <div class="col-sm-7">
                <div class="row">
                    <div class="col-lg-2 float-right ml-auto">
                        <img class="img-fluid" src="../img/kdp.png" alt="" />
                    </div>
                    <div class="col-lg-3 float-right ml-auto">
                        <img class="img-fluid" src="../img/mjn.png" alt="" />
                    </div>
                    <div class="col-lg-3 float-right ml-auto">
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
    return {
    isMap: this.$route.path === "/map",
    }
    },
    }</script>
<style>
</style>
