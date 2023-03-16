<template>
    <Bar :data="chartData" :options="chartOptions" ></Bar>
</template>

<script>
import { Bar } from 'vue-chartjs'
import { Chart, Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale } from 'chart.js'
Chart.register(Title, Tooltip, Legend, BarElement, CategoryScale, LinearScale)

export default {
    components: { Bar },
    props: ['chartData'],
    data() {
        return {
            chartOptions: {
                responsive: true,
                maintainAspectRatio: false
            }
        }
    }
};
</script>
  