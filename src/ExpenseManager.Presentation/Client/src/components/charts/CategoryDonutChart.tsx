import {Cell, Legend, Pie, PieChart, ResponsiveContainer} from 'recharts';
import {Card, CardContent, CardHeader, CardTitle} from "@/components/ui/card.tsx";
import {ChartsDto} from "@/models/charts/ChartsDto.ts";

const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042', '#F2545B']

export function CategoryDonutChart({type, data}: { type: 'expense' | 'income', data: ChartsDto }) {
    const donutChartData = data[`${type}CategoryDonutChart`]
    const title = type === 'expense' ? 'Expense Categories' : 'Income Categories'

    return (
        <Card className="w-full sm:w-[400px] h-[400px]">
            <CardHeader>
                <CardTitle>{title}</CardTitle>
            </CardHeader>
            <CardContent>
                <ResponsiveContainer width="100%" height={300}>
                    <PieChart>
                        <Pie
                            data={donutChartData}
                            dataKey={'value'}
                            innerRadius={50}
                            outerRadius={80}
                            label
                        >
                            {
                                donutChartData.map((_, index) =>
                                    <Cell key={`cell-${index}`}
                                          fill={COLORS[(index % COLORS.length) + (Math.floor(index / COLORS.length))]}/>)
                            }
                        </Pie>
                        <Legend/>

                    </PieChart>
                </ResponsiveContainer>
            </CardContent>
        </Card>
    )
}