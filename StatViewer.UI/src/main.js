import { createApp } from 'vue'
import App from './App.vue'

import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'

createApp(App).use('VueDatePicker', VueDatePicker).mount('#app')
