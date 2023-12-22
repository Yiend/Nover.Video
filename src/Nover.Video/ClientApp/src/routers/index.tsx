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
		path: "/down/bilibili",
		element: lazyLoad(React.lazy(() => import("@/views/bilibili/index"))),
		meta: {
			title: "哔哩哔哩下载",
			key: "bilibili"
		}
	 },
	 {
		path: "/down/xhs",
		element: lazyLoad(React.lazy(() => import("@/views/xhs/index"))),
		meta: {
			title: "小红书下载",
			key: "xhs"
		}
	 },
	 {
		path: "/down/tiktok",
		element: lazyLoad(React.lazy(() => import("@/views/tiktok/index"))),
		meta: {
			title: "抖音下载",
			key: "tiktok"
		}
	 },
	 {
		path: "/down/youtube",
		element: lazyLoad(React.lazy(() => import("@/views/youtube/index"))),
		meta: {
			title: "油管下载",
			key: "youtube"
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