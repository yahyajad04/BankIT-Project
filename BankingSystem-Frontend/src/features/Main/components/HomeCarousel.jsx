export default function HomeCarousel(props){
    return (
        <div
            id={`carousel-${props.tag}`}
            className="carousel slide"
            data-bs-ride="false"
        >
            <div className="carousel-inner">

                {props.images.map((image, index) => (
                    <div
                        key={index}
                        className={`carousel-item ${index === 0 ? "active" : ""}`}
                    >
                        <img
                            src={image}
                            className="d-block w-100 card-img-top"
                            alt={`${props.title} ${index + 1}`}
                        />
                    </div>
                ))}

            </div>

            {/* Previous */}
            <button
                className="carousel-control-prev"
                type="button"
                data-bs-target={`#carousel-${props.tag}`}
                data-bs-slide="prev"
            >
                <span className="carousel-control-prev-icon"></span>
                <span className="visually-hidden">Previous</span>
            </button>

            {/* Next */}
            <button
                className="carousel-control-next"
                type="button"
                data-bs-target={`#carousel-${props.tag}`}
                data-bs-slide="next"
            >
                <span className="carousel-control-next-icon"></span>
                <span className="visually-hidden">Next</span>
            </button>

        </div>

    )
}