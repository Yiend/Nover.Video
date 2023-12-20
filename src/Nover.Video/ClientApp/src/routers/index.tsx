import React from "react";
import { useRoutes } from "react-router-dom";
import Layout from "@/layout/index";
import lazyLoad from "@/routers/lazyLoad"

interface MetaProps {
	keepAlive?: boolean;
	title: string;
	key?: string;
}

interface RouteObject {
	caseSensitive?: boolean;
	children?: RouteObject[];
	element?: React.ReactNode;
	index?: boolean;
	path?: string;
	meta?: MetaProps;
	isLink?: string;
}

const router: Array<RouteObject>= [
  {
	path:'/',
    element: <Layout/>,
	meta: {
			title: "首页"
    },
    children:[
      {
			path: "/",
			element: lazyLoad(React.lazy(() => import("@/views/home/index"))),
			meta: {
				title: "首页",
				key: "home"
			}
	 },
     {
			path: "/setting/downloadSetting",
			element: lazyLoad(React.lazy(() => import("@/views/setting/index"))),
			meta: {
				title: "下载设置",
				key: "setting"
			}
	  },
    ]
  },
];

const Router = () => {
	const routes = useRoutes(router);
	return routes;
};

export default Router;