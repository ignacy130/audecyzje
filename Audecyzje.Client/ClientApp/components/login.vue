<template>
    <div class="container">
        <div class="col-6 py-5 mx-auto">
            <h2 class="mb-2">Zaloguj się</h2>
            <form @submit.prevent="login" autocomplete="off">
                <div class="form-group with-icon-right">
                    <div class="input-group">
                        <label for="password">Email</label>
                        <input id="email"
                               name="email"
                               v-model="email"
                               class="form-control"
                               required />
                    </div>
                </div>
                <div class="input-group">
                    <label for="password">Hasło</label>
                    <input id="password" v-model="password" class="form-control" type="password" />
                </div>
                <button type="submit" class="btn btn-success form-control mt-2">Zaloguj</button>
            </form>
        </div>
    </div>
</template>

<script>
    import router from 'vue-router'

    export default {
        name: 'Login',
        data() {
            return {
                email: "",
                password: ""
            }
        },
        methods: {
            async login() {
                var result = await this.$http.post('/api/account/', { email: this.email, password: this.password });
                let redirectTo = '/admin'
                this.$router.push(redirectTo)
            }
        }
    }
</script>
