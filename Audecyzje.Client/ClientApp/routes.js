import CounterExample from 'components/counter-example'
import Map from 'components/map-page'
import HomePage from 'components/home-page'
import PageNotFound from 'components/page-not-found'

export const routes = [
    { path: '/', component: HomePage, display: 'Home', style: 'glyphicon glyphicon-home' },
    { path: '/counter', component: CounterExample, display: 'Counter', style: 'glyphicon glyphicon-education' },
    { path: '/map', component: Map, display: 'Mapa reprywatyzacji', style: 'glyphicon glyphicon-th-list' },
    { path: "*", component: PageNotFound }
]
