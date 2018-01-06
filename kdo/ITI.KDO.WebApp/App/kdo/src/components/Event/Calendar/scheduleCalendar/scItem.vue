<template>
    <div class="schedule-calendar-detail-item"
         draggable
         @dragstart="onDrag"
         @click="func()"
         :id="'exPopover-' + this.eId" variant="primary">
        <span class="schedule-calendar-detail-text" >{{ item.eventName }}</span>
        <b-popover 
            :target = "'exPopover-' + this.eId"
            title="Event Information"
            triggers="hover focus"
            content="Embedding content using properties is easy">
            <div>
                <b-form-group label="Name" label-for="pop1" horizontal class="mb-1">
                <b-form-input id="pop1" size="sm" :value="this.item.eventName" disabled/>
                </b-form-group>
                <b-form-group label="Date" label-for="pop2" horizontal class="mb-1">
                <b-form-input id="pop2" size="sm" :value="this.item.dates" disabled/>
                </b-form-group>
                <strong>Descriptions</strong>
                <b-alert show class="small">
                    {{ this.item.descriptions }}
                </b-alert>
            </div>
        </b-popover>
    </div>
</template>
<script>
export default {
    data() {
        return {
            eId: null
        }
    },
    async mounted(){
        this.eId = this.item.eventId;
        console.log(this.eId);
    },
    props: {
        item: Object,
        date: Date,
        type: String,
    },
    methods: {
        onDrag(e) {
            this.$emit('item-dragstart', e, this.item, this.date, this.type);
        },
        func(){
            console.log(this.item.eventId);
        }
    }
}
</script>
<style lang="less">
@import './variables.less';

.schedule-calendar-detail-item {
    padding: 0 5px;
    margin: 3px 6px 0;
    font-size: 12px;
    color: #fff;
    line-height: 2em;
    border-radius: 2px;
    background: @sc-primary-color;
    cursor: pointer;
    overflow: hidden;
    white-space: nowrap;
    text-overflow: ellipsis;
    transition: .2s ease-in-out;

    &:hover {
        transform: translateY(-2px);
        box-shadow: 0 3px 8px rgba(0, 0, 0, .2), 0 -3px 8px rgba(0, 0, 0, .2)
    }
}
</style>
