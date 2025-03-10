DROP TABLE IF EXISTS default.cerial;
CREATE TABLE default.cerial (
    id INT NOT NULL AUTO_INCREMENT,
    cerial_name VARCHAR(255),
    mfr VARCHAR(255),
    cerial_type VARCHAR(255),
    calories int,
    protein int,
    fat int,
    sodium int,
    fiber float,
    carbo float,
    sugars int,
    potass int,
    vitamins int,
    shelf int,
    weight float,
    cups float,
    rating float,
    PRIMARY KEY (id)
)