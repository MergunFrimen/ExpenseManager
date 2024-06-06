import {
    Card,
    CardDescription,
    CardHeader,
    CardTitle,
} from "@/components/ui/card"

export function TotalBalance() {
    return (
        <Card className="w-[300px] h-[120px]">
            <CardHeader className="pb-2">
                <CardDescription>Total balance</CardDescription>
                <CardTitle className="text-4xl">$1,329</CardTitle>
            </CardHeader>
            {/*<CardContent>*/}
            {/*    <div className="text-xs text-muted-foreground">+25% from last week</div>*/}
            {/*</CardContent>*/}
            {/*<CardFooter>*/}
            {/*    <Progress value={25} aria-label="25% increase" />*/}
            {/*</CardFooter>*/}
        </Card>
    )
}
