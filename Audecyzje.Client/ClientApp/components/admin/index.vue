<template>
    <div class="container">
        <div class="col-12">
            <router-link to="/admin/posts/add" class="btn btn-success">
                Dodaj post
            </router-link>
        </div>
        <div class="col-12 mt-3">
            <h3>Posty</h3>
            <div v-for="post in posts" v-bind:class="[post.id == selectedArticle.id ? 'link-selected' : '']">
                <a :href='"/admin/posts/edit/"+post.id' class="mt-3 mb-1" v-if="post.parentId === -1" v-on:click="selectArticle(post.id)"><strong>{{post.title}}</strong></a>
                <a :href='"/admin/posts/edit/"+post.id' class="ml-2 my-1" v-on:click="selectArticle(post.id)" v-else>{{post.title}}</a>
            </div>
        </div>
        <div class="col-12 mt-5">
            <button @click="logoff" class="btn btn-sm btn-danger">Wyloguj się</button>
        </div>
    </div>
</template>

<script>
    export default {
        name: 'AdminIndex',
        data() {
            return {
                posts: []
            };
        },
        async created() {
            var result = await this.$http.get('/api/posts/GetAll/');
            if (result.status === 200) {
                this.posts = result.data;
            }
        },
        methods: {
            selectedArticle(id) {

            },
            async logoff() {
                var result = await this.$http.post('/api/account/logoff/');
                let redirectTo = '/admin/login'
                this.$router.push(redirectTo)
            }
        }
    }
</script>
