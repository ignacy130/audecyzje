<template>
    <div class="container">
        <div class="col-12 py-5 mx-auto">
            <h2>Dodaj post</h2>
            <div>
                <div class="form-group">
                    <label for="title">Tytuł posta</label>
                    <input type="text" class="form-control" v-model="post.title" id="title">
                </div>
                <div class="form-group">
                    <label for="content">Treść</label>
                    <medium-editor :text='post.content' :options='options' v-on:edit='processEditOperation' custom-tag='div'>
                    </medium-editor>
                </div>
                <div class="form-group">
                    <label for="parent">Wybierz post nadrzędny</label>
                    <select class="form-control" v-model="post.parentId" id="parent">
                        <option v-for="parent in posts" v-bind:value="parent.id">
                            {{ parent.title }}
                        </option>
                    </select>
                </div>
                <div class="form-group form-check">
                    <input type="checkbox" class="form-check-input" v-model="post.isPublished" id="published">
                    <label class="form-check-label" for="published">Opublikowany?</label>
                </div>
                <button v-on:click="save" class="btn btn-primary">Zapisz post</button>
            </div>
        </div>
    </div>
</template>

<script>
    import editor from 'vue2-medium-editor'

    export default {
        components: {
            'medium-editor': editor
        },
        name: 'AddPost',
        data() {
            return {
                options: {
                    toolbar: { buttons: ['bold', 'strikethrough', 'h1'] }
                },
                post: {
                    title: '',
                    content: '',
                    isPublished: false,
                    parentId: -1
                },
                posts: []
            }
        },
        methods: {
            async save() {
                var result = await this.$http.post('/posts/Create', this.post);
            },
            processEditOperation: function (operation) {
                this.post.content = operation.api.origElements.innerHTML;
            }
        },
        async created() {
            var result = await this.$http.get('/api/posts/GetAll/');
            this.posts = result.data;
        },
    }
</script>
