<template>
    <div class="div-style">
        <VueDatePicker class="date-picker" v-model="startDate" :format="'yyyy-MM-dd'"></VueDatePicker>
        <VueDatePicker class="date-picker" v-model="endDate" :format="'yyyy-MM-dd'"></VueDatePicker>
        <button @click="fetchStatistics">Fetch Statistics</button>
    </div>
</template>

<style>
.div-style {
    display: inline-flex;
}

.date-picker {
    margin-right: 2rem;
    width: 10rem;
}
</style>

<script>
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'

export default
{
    name: 'ChartComponent',
    components: { VueDatePicker },
    methods: {
        async fetchStatistics() {
            try {
                const storeId = 1;
                const startDateFormated = this.startDate.toLocaleDateString('en-Ca', { year: 'numeric', month: '2-digit', day: '2-digit' })
                const endDateFormated = this.endDate.toLocaleDateString('en-Ca', { year: 'numeric', month: '2-digit', day: '2-digit' })
                const response = await fetch(`https://localhost:7058/statistics/${storeId}?startDate=${startDateFormated}&endDate=${endDateFormated}`);
                const statistics = await response.json();
                this.$emit("statistics-fetched", statistics);
            } catch (error) {
                console.error(error);
            }
        }
    },
    data() {
        return {
            startDate: new Date(),
            endDate: new Date(),
        }
    }
}
</script>