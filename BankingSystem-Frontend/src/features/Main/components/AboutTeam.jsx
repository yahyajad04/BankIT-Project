import AboutMember from "./AboutMember"

export default function AboutTeam(){
    return (
        <section className="py-5 bg-light">
            <div className="container px-5 my-5">
                <div className="text-center">
                    <h2 className="fw-bolder">Our team</h2>
                    <p className="lead fw-normal text-muted mb-5">Dedicated to quality and your success</p>
                </div>
                <div className="row gx-5 row-cols-1 row-cols-sm-2 row-cols-xl-4 justify-content-center">
                    <AboutMember 
                        image="https://dummyimage.com/150x150/ced4da/6c757d"
                        name="Yahya Jad"
                        position="Founder &amp; CEO"/>
                    <AboutMember 
                        image="https://dummyimage.com/150x150/ced4da/6c757d"
                        name="Mohammed Khaled"
                        position="Senior Computer Engineer"/>
                    <AboutMember 
                        image="https://dummyimage.com/150x150/ced4da/6c757d"
                        name="Rami Ali"
                        position="Accountant"/>
                    <AboutMember 
                        image="https://dummyimage.com/150x150/ced4da/6c757d"
                        name="Sami Raed"
                        position="Accountant"/>
                </div>
            </div>
        </section>
    )
}