<template>
    <div class="container">
        <div class="col-12 py-5 mx-auto">
            <h2>Zaloguj się</h2>
            <form @submit.prevent="login" autocomplete="off">
                <div class="form-group with-icon-right">
                    <div class="input-group">
                        <input id="email"
                               name="email"
                               v-model="email"
                               required />
                        <i class="fa fa-exclamation-triangle error-icon icon-right input-icon"></i>
                        <i class="fa fa-check valid-icon icon-right input-icon"></i>
                    </div>
                </div>

                <label for="password">Password</label>
                <input id="password" v-model="password" placeholder="password" type="password" />
                <button type="submit">login</button>
            </form>
        </div>
    </div>
</template>

<script>
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
                var result = await this.$http.post('/api/account/', { email: this.email, password: this.password});
            }
        }
    }
</script>
