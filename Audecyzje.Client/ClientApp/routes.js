import Map from 'components/map-page'
import HomePage from 'components/home-page'
import PageNotFound from 'components/page-not-found'
import BazaWiedzy from 'components/baza-wiedzy'
import Lokatorzy from 'components/lokatorzy'
import Decyzja from 'components/decyzja'
import Contact from 'components/contact'
import AdminIndex from 'components/admin/index'
import AddPost from 'components/admin/posts/add'
import EditPost from 'components/admin/posts/edit'
import Login from 'components/login'

export const routes = [
    { path: '/', component: HomePage, display: 'Home', style: 'glyphicon glyphicon-home' },
    { path: '/admin/login', component: Login, display: 'Login', style: 'glyphicon glyphicon-th-list' },
    { path: '/baza-wiedzy', component: BazaWiedzy, display: 'Baza wiedzy', style: 'glyphicon glyphicon-th-list' },
    { path: '/lokatorzy', component: Lokatorzy, display: 'Reprywatyzacja a lokatorzy', style: 'glyphicon glyphicon-th-list' },
    { path: '/decyzja', component: Decyzja, display: 'Jak przeanalizować decyzję', style: 'glyphicon glyphicon-th-list' },
    { path: '/mapa', name: 'map', component: Map, display: 'Mapa reprywatyzacji', style: 'glyphicon glyphicon-th-list', props: {query: false} },
    { path: '/kontakt', component: Contact, display: 'Kontakt', style: 'glyphicon glyphicon-th-list' },
    { path: '/admin/', component: AdminIndex, display: 'Admin', style: 'glyphicon glyphicon-th-list' },
    { path: '/admin/posts/add', component: AddPost, display: 'Dodaj post', style: 'glyphicon glyphicon-th-list' },
    { path: '/admin/posts/edit/:id', component: EditPost, display: 'Edytuj post', style: 'glyphicon glyphicon-th-list' },
    { path: "*", component: PageNotFound },
]

