export interface ChartsDto {
    expenseCategoryDonutChart: DonutChartDto[];
    incomeCategoryDonutChart: DonutChartDto[];
}

export interface DonutChartDto {
    category: string;
    total: number;
}
