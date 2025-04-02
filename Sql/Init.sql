CREATE TABLE vehicle_type
(
    number smallint PRIMARY KEY,
    name   varchar(30)
);

CREATE TABLE operators
(
    number smallint PRIMARY KEY,
    name   varchar(40) UNIQUE
);

CREATE TABLE routes
(
    number   varchar(6) PRIMARY KEY,
    name     varchar(70) UNIQUE,
    operator smallint REFERENCES operators (number) NOT NULL
);

CREATE TABLE vehicles
(
    number numeric(4) PRIMARY KEY,
    type   smallint REFERENCES vehicle_type (number) NOT NULL
);

CREATE TABLE rides
(
    id      uuid PRIMARY KEY,
    vehicle numeric(6) REFERENCES vehicles (number) NOT NULL,
    route   varchar(6) REFERENCES routes (number)   NOT NULL
);

CREATE TABLE bank_transactions
(
    bin     numeric(6),  -- МФО банку
    account numeric(14), -- Рахунок
    amount  int,
    time    timestamptz,
    ride    uuid UNIQUE REFERENCES rides (id) PRIMARY KEY
);

CREATE TABLE cards_owners
(
    id          SERIAL PRIMARY KEY,
    first_name  varchar(30) NOT NULL,
    last_name   varchar(30) NOT NULL,
    middle_name varchar(30),
    birth_date  date        NOT NULL
);

CREATE TABLE travel_cards
(
    code            numeric(10) PRIMARY KEY,
    owner           int REFERENCES cards_owners (id),
    release_date    date NOT NULL,
    expiration_date date NOT NULL
);

CREATE TABLE card_operation
(
    card   numeric(10) REFERENCES travel_cards (code),
    ride   uuid REFERENCES rides (id),
    date   timestamptz NOT NULL,
    change int
);

-- Якщо поповнються картка, то додатна кількість у change і відсутній привʼязана поїздка.
-- Якщо це поїздка, то change відʼємний і обовʼязково привʼязана поїздка.
ALTER TABLE card_operation
    ADD CONSTRAINT ride_ref_w_change_check CHECK ((ride IS NOT NULL AND change < 0) OR (ride IS NULL AND change > 0));
