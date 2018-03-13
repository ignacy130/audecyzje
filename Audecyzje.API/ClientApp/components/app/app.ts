import Vue from 'vue';
import { Component } from 'vue-property-decorator';
import 'bootstrap';

@Component({
    components: {
		MenuComponent: require('../navmenu/navmenu.vue.html'),
		FooterComponent: require('../footer/footer.vue.html')
    }
})
export default class AppComponent extends Vue {
}
