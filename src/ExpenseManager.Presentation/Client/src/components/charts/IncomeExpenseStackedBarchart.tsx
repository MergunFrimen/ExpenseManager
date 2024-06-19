// import {
//     Bar,
//     BarChart,
//     CartesianGrid,
//     Legend,
//     ReferenceLine,
//     ResponsiveContainer,
//     Tooltip,
//     XAxis,
//     YAxis
// } from 'recharts';
// import {Card, CardContent, CardDescription, CardFooter, CardHeader, CardTitle} from "@/components/ui/card.tsx";
// import {RadioGroup, RadioGroupItem} from '../ui/radio-group';
// import {Label} from "@/components/ui/label.tsx";
// import {useTheme} from "@/components/theme/ThemeProvider.tsx";
//
// const data = [
//     {
//         name: 'January',
//         income: 4000,
//         expense: -2400
//     },
//     {
//         name: 'February',
//         income: 3000,
//         expense: -1398
//     },
//     {
//         name: 'March',
//         income: 2000,
//         expense: -3800
//     },
//     {
//         name: 'April',
//         income: 2780,
//         expense: -3908
//     },
//     {
//         name: 'May',
//         income: 1890,
//         expense: -4800
//     },
//     {
//         name: 'June',
//         income: 2390,
//         expense: -3800
//     },
//     {
//         name: 'July',
//         income: 3490,
//         expense: -4300
//     },
//     {
//         name: 'August',
//         income: 3490,
//         expense: -4300
//     },
//     {
//         name: 'September',
//         income: 3490,
//         expense: -4300
//     },
//     {
//         name: 'October',
//         income: 3490,
//         expense: -4300
//     },
//     {
//         name: 'November',
//         income: 3490,
//         expense: -4300
//     },
//     {
//         name: 'December',
//         income: 3490,
//         expense: -4300
//     },
// ];
//
// export default function IncomeExpenseStackedBarchart() {
//     const {theme} = useTheme();
//
//     const fillColor = theme === "light" ? "hsl(240 4.8% 95.9%)" : "hsl(240 3.7% 15.9%)";
//
//     return (
//         <Card className="w-full sm:w-[824px] h-[800px]">
//             <CardHeader>
//                 <CardTitle>Title</CardTitle>
//             </CardHeader>
//             <CardDescription>
//                 Description
//             </CardDescription>
//             <CardContent>
//                 <ResponsiveContainer width="100%" height={300}>
//                     <BarChart
//                         data={data}
//                         stackOffset="sign"
//                         margin={{
//                             top: 5,
//                             right: 30,
//                             left: 20,
//                             bottom: 5,
//                         }}
//                     >
//                         <CartesianGrid strokeDasharray="3 3"/>
//                         <XAxis dataKey="name"/>
//                         <YAxis/>
//                         <Tooltip cursor={{fill: fillColor}} content={<CustomTooltip/>}/>
//                         <Legend/>
//                         <ReferenceLine y={0} stroke="#000"/>
//                         <Bar dataKey="income" fill="#8884d8" stackId="stack"
//                             // activeBar={<Rectangle fill="pink" stroke="blue"/>}
//                         />
//                         <Bar dataKey="expense" fill="#82ca9d" stackId="stack"
//                             // activeBar={<Rectangle fill="gold" stroke="purple"/>}
//                         />
//                     </BarChart>
//                 </ResponsiveContainer>
//                 <CardFooter>
//                     <RadioGroup defaultValue="comfortable">
//                         <div className="flex items-center space-x-2">
//                             <RadioGroupItem value="default" id="r1"/>
//                             <Label htmlFor="r1">This week</Label>
//                         </div>
//                         <div className="flex items-center space-x-2">
//                             <RadioGroupItem value="comfortable" id="r2"/>
//                             <Label htmlFor="r2">This month</Label>
//                         </div>
//                         <div className="flex items-center space-x-2">
//                             <RadioGroupItem value="compact" id="r3"/>
//                             <Label htmlFor="r3">This year</Label>
//                         </div>
//                     </RadioGroup>
//                 </CardFooter>
//             </CardContent>
//         </Card>
//
//     )
// }
//
// function CustomTooltip({active, payload}) {
//     if (active && payload && payload.length) {
//         return (
//             <div className="custom-tooltip bg-background p-3 rounded-md">
//                 <p className="label">{`Income : ${payload[0].value}`}</p>
//                 <p className="label">{`Expense : ${payload[1].value}`}</p>
//             </div>
//         );
//     }
//
//     return null;
// }
