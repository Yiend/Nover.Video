import { HashRouter } from "react-router-dom";
import Router from "@/routers/index";
import { ThemeProvider } from "@/components/theme-provider"
import './App.css'
import './styles/globals.css'

function App() {
  return (
    <HashRouter>
      <ThemeProvider defaultTheme="system" storageKey="vite-ui-theme">
        <Router />
      </ThemeProvider>
		</HashRouter>
  )
}
export default App
