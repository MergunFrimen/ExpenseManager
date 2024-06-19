import {Cell, Legend, Pie, PieChart, ResponsiveContainer} from 'recharts';
import {Card, CardContent, CardHeader, CardTitle} from "@/components/ui/card.tsx";
import {useAuth} from "@/components/auth/AuthProvider.tsx";
import useSWR from 'swr';
import {Skeleton} from '../ui/skeleton';

const COLORS = ['#0088FE', '#00C49F', '#FFBB28', '#FF8042', '#F2545B']

async function fetcher(url: string, token: string | null) {
    const response = await fetch(url, {
        method: "GET",
        headers: {
            "Content-Type": "application/json",
            "Authorization": "Bearer " + token
        }
    });

    if (!response.ok)
        throw response;

    return await response.json();
}

export function CategoryDonutChart({type}: { type: 'expense' | 'income' }) {
    const {token} = useAuth();
    const {data, isLoading} = useSWR(
        ["/api/v1/statistics/charts", token],
        ([url, token]) => fetcher(url, token)
    );

    if (isLoading)
        return <Skeleton className="w-full sm:w-[400px] h-[400px]"/>

    const title = type === 'expense' ? 'Expense Categories' : 'Income Categories'
    const donutChartData = data[`${type}CategoryDonutChart`]

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
                            animationBegin={0}
                            animationDuration={800}
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