import Ember from 'ember';

export default Ember.Controller.extend({
    isOpen: false,
    actions: {
        toggleNavigation() {
            this.toggleProperty('isOpen');
        }
    }
});
