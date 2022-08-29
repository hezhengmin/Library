import Vue from 'vue';
import Vuex from 'vuex';

Vue.use(Vuex);

export default new Vuex.Store({
    state: {
        count: 0,
        //登入成功的token
        jwtToken: '' 
    },
    mutations: {
        increment(state) {
            state.count++
        },
        //token 
        setJwtToken(state, token) {
            state.jwtToken = token;
        }
    },
    actions: {
        fetchAccessToken({ commit }) {
            commit('setJwtToken', localStorage.getItem('jwtToken'));
        }
    }
})