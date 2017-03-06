import Vue from 'vue'
import VueRouter from 'vue-router'
import App from './App.vue'
import Dashboard from './pages/Dashboard.vue'

Vue.use(VueRouter);

const routes = [
    { path: '/', component: App },
    { path: '/dashboard', component: Dashboard }
]

export default new VueRouter({
  routes: routes
})
