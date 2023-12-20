import { Outlet } from "react-router-dom";
import { Header } from "./components/header";
import { Sidebar } from "./components/sidebar";

export default function Layout() {
  return (
    <>
     <div className="overflow-hidden rounded-[0.5rem] border bg-background shadow">    
        <div className="hidden md:block">
            <Header/>
            <div className="border-t">
                <div className="bg-background">
                    <div className="grid lg:grid-cols-5">
                        <Sidebar className="hidden lg:block" />
                        <div className="col-span-3 lg:col-span-4 lg:border-l">
                            <div className="h-full px-4 py-6 lg:px-8">
                                <Outlet/>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
      </div>
    </>
  )
}
