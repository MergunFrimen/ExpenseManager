import {Bar, BarChart, CartesianGrid, Legend, Rectangle, ReferenceLine, ResponsiveContainer, Tooltip, XAxis, YAxis} from 'recharts';

const data = [
    {
        name: 'January',
        income: 4000,
        expense: -2400
    },
    {
        name: 'February',
        income: 3000,
        expense: -1398
    },
    {
        name: 'March',
        income: 2000,
        expense: -3800
    },
    {
        name: 'April',
        income: 2780,
        expense: -3908
    },
    {
        name: 'May',
        income: 1890,
        expense: -4800
    },
    {
        name: 'June',
        income: 2390,
        expense: -3800
    },
    {
        name: 'July',
        income: 3490,
        expense: -4300
    },
    {
        name: 'August',
        income: 3490,
        expense: -4300
    },
    {
        name: 'September',
        income: 3490,
        expense: -4300
    },
    {
        name: 'October',
        income: 3490,
        expense: -4300
    },
    {
        name: 'November',
        income: 3490,
        expense: -4300
    },
    {
        name: 'December',
        income: 3490,
        expense: -4300
    },
];

export default function IncomeExpenseStackedBarchart() {
    return (
        <ResponsiveContainer width="100%" height="100%">
            <BarChart
                width={500}
                height={300}
                data={data}
                stackOffset="sign"
                margin={{
                    top: 5,
                    right: 30,
                    left: 20,
                    bottom: 5,
                }}
            >
                <CartesianGrid strokeDasharray="3 3" />
                <XAxis dataKey="name" />
                <YAxis />
                <Tooltip />
                <Legend />
                <ReferenceLine y={0} stroke="#000" />
                <Bar dataKey="income" fill="#8884d8" stackId="stack" activeBar={<Rectangle fill="pink" stroke="blue" />} />
                <Bar dataKey="expense" fill="#82ca9d" stackId="stack" activeBar={<Rectangle fill="gold" stroke="purple" />} />
            </BarChart>
        </ResponsiveContainer>
    )
}
