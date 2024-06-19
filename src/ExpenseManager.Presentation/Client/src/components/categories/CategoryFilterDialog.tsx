import {Form, FormControl, FormField, FormItem, FormLabel, FormMessage} from "@/components/ui/form.tsx";
import {Input} from "@/components/ui/input.tsx";
import {Dialog, DialogContent, DialogHeader, DialogTitle, DialogTrigger} from "@/components/ui/dialog.tsx";
import {Button} from "@/components/ui/button.tsx";
import {FilterIcon} from "lucide-react";
import {useState} from "react";

export function CategoryFilterDialog({form, onSubmit}: {
    form: any,
    onSubmit: any,
}) {
    const [open, setOpen] = useState(false);

    return (
        <Dialog open={open} onOpenChange={setOpen}>
            <DialogTrigger asChild>
                <Button variant="secondary" size="icon">
                    <FilterIcon className="h-[1.2rem] w-[1.2rem]"/>
                </Button>
            </DialogTrigger>
            <DialogContent className="sm:max-w-[425px]" onInteractOutside={(e) => e.preventDefault()}>
                <DialogHeader>
                    <DialogTitle>Filter categories</DialogTitle>
                </DialogHeader>
                <Form {...form}>
                    <form onSubmit={form.handleSubmit(onSubmit)}
                          className="space-y-8 flex flex-col size-full items-center justify-center">
                        <FormField
                            control={form.control}
                            name="name"
                            render={({field}) => (
                                <FormItem className="size-full">
                                    <FormLabel htmlFor={'name'}>Name</FormLabel>
                                    <FormControl>
                                        <Input
                                            id="name"
                                            defaultValue="@peduarte"
                                            className="col-span-3"
                                            {...field}
                                        />
                                    </FormControl>
                                    <FormMessage/>
                                </FormItem>
                            )}
                        />
                        <Button type="submit" className={'w-full'} onClick={() => {
                            setOpen(false);
                        }}>Apply filter</Button>
                    </form>
                </Form>
            </DialogContent>
        </Dialog>
    )
}