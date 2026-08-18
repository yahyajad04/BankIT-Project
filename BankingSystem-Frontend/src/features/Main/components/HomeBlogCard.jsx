import HomeCarousel from "./HomeCarousel"

export default function HomeBlogCard(props){
    return(
        <div className="col-lg-4 mb-5">
            <div className="card h-100 shadow border-0">
                 <HomeCarousel 
                    tag={props.tag}
                    images={props.images}/>

                <div className="card-body p-4">
                    <div className="badge background-main bg-gradient rounded-pill mb-2" style={{color:"black"}}>{props.tag}</div>
                    <h5 className="card-title mb-3">{props.title}</h5>
                    <p className="card-text mb-0">{props.body}</p>
                </div>
                <div className="card-footer p-4 pt-0 bg-transparent border-top-0">
                    <div className="d-flex align-items-end justify-content-between">
                        <div className="d-flex align-items-center">
                            <img className="rounded-circle me-3" src="https://dummyimage.com/40x40/ced4da/6c757d" alt="..." />
                            <div className="small">
                                <div className="fw-bold">{props.auther}</div>
                                <div className="text-muted">{props.date}</div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}