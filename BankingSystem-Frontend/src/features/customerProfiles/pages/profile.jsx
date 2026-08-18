import { useOutletContext } from "react-router-dom";
import ProfileCard from "../components/ProfileCard";
import InfoCard from "../components/InfoCard";
import Spinner from "../../../components/common/Spinner";

export default function Profile() {
    const {profile, loading} = useOutletContext();

    return (
        <div>
            {loading ? <Spinner /> : profile ? <section
                className="w-100 p-4"
                style={{
                    backgroundColor: "#eee",
                    borderRadius: ".5rem .5rem 0 0",
                }}
            >
                <div className="row">
                    
                    <ProfileCard 
                    name={profile.appUserDTO.name}
                    email={profile.appUserDTO.email}
                    country={profile.country}
                    city={profile.city}/>

                    <InfoCard 
                        nationalId={profile.nationalId}
                        userName={profile.appUserDTO.userName}
                        email={profile.appUserDTO.email}
                        address={profile.address}
                        postalCode={profile.postalCode}/>

                </div>
            </section> : 
            <div>There is a problem occured</div>}
        </div>
    );
}