<template>
    <footer class="container pt-5" v-bind:class="{ 'hide': isMap }">
        <div class="row">
            <p class="col-sm-3">
                Copyright &copy; 2017
                <br>Miasto Jest Nasze</p>
            <p class="col-sm-2">
                <a href="/privacy-policy">Polityka Prywatności</a>
            </p>
            <p class="col-sm-2">
                <a href="/termsofuse">Regulamin</a>
            </p>
            <ul class="col-sm-5">
                <li class="col-lg-2">
                    <img class="img-fluid" src="" alt=""/>
                </li>
                <li class="col-lg-2">
                    <img class="img-fluid" src="" alt=""/>
                </li>
            </ul>
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
    }
</script>

<style>
</style>
