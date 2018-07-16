<template>
    <div>
        <input id="street-search" v-model="streetSearched" @change="searchStreetChange" />
        <l-map :zoom="zoom" :center="center" style="height: 1000px; width: 1000px;">
            <v-geosearch :options="geosearchOptions"></v-geosearch>
            <l-tile-layer :url="url" :attribution="attribution"></l-tile-layer>
        </l-map>
    </div>
</template>

<script>
    import { LMap, LTileLayer } from 'vue2-leaflet';
    import { OpenStreetMapProvider } from 'leaflet-geosearch';
    import { VGeosearch } from 'vue2-leaflet-geosearch';

    export default {
        name: 'MapComponent',
        components: {
            LMap,
            LTileLayer,
            VGeosearch
        },
        data() {
            return {
                streetSearched: "",
                zoom: 13,
                center: [52.2297, 21.0122],
                url: 'http://{s}.tile.osm.org/{z}/{x}/{y}.png',
                attribution: '&copy; <a href="http://osm.org/copyright">OpenStreetMap</a> contributors',
                marker: L.latLng(52.2297, 21.0122),
                geosearchOptions: {
                    provider: new OpenStreetMapProvider(),
                },
            }
        },
        methods: {
            async searchStreetChange() {
                var results = await this.geosearchOptions.provider.search({ query: input.value });
                console.log(results);
            }
        }
    }
</script>
<style>
    @import "~leaflet/dist/leaflet.css";
</style>
