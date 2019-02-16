<template>
    <div class="container">
        <div class="col-6 py-5 mx-auto">
            <h2 class="mb-2">Zarejestruj nowego użytkownika</h2>
            <form @submit.prevent="AddAccount" autocomplete="off">
                <div class="form-group with-icon-right">
                    <div class="input-group">
                        <label for="Email">Email</label>
                        <input id="email" name="email" v-model="user.Email" class="form-control" required />
                    </div>
                </div>
                <div class="input-group">
                    <label for="password">Hasło</label>
                    <input id="password" v-model="user.Password" class="form-control" type="password" />
                </div>
                <button type="submit" class="btn btn-success form-control mt-2">Zarejestruj</button>
            </form>
        </div>
    </div>
</template>

<script>
    import router from 'vue-router'

    export default {
        name: 'Register',
        data() {
            return {
                user: {
                    Email: '',
                    Password: ''
                },
                hasErrors: false
            };
        },
        methods: {
            async AddAccount() {
                try {
                    this.hasErrors = false;
                    const { Email, Password } = this.user;

                    if (Email && Password) {
                        const { data } = await this.$http.post('/api/account/register', { Email: Email, Password: Password })
                        let redirectTo = '/admin/'
                        this.$router.push(redirectTo)
                    }
                } catch (err) {
                    this.hasErrors = true;
                    throw Error('Client-side validation failed' + err);
                }
            }
        }
    }
</script>
