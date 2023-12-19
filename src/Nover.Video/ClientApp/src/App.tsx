import { ThemeProvider } from "@/components/theme-provider"
import { ModeToggle } from "@/components/mode-toggle"
import Home from "./views/home"
import './App.css'
import './styles/globals.css'

function App() {
  return (
    <>
     <ThemeProvider defaultTheme="system" storageKey="vite-ui-theme">
       <ModeToggle></ModeToggle>
       <Home></Home>     
    </ThemeProvider>      
    </>
  )
}

export default App
