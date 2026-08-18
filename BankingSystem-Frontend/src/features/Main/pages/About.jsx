import AboutHeader from "../components/AboutHeader"
import AboutStory from "../components/AboutStory"
import AboutGrowth from "../components/AboutGrowth"
import AboutTeam from "../components/AboutTeam"

export default function AboutPage(){
    return (
    <main className="flex-shrink-0">
        <AboutHeader />
        
        <AboutStory />

        <AboutGrowth />

        <AboutTeam />   
    </main>
    )
}