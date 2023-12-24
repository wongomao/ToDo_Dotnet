create table if not exists todo (
    todo_id INTEGER PRIMARY KEY,
    description TEXT NOT NULL,
    done BOOLEAN NOT NULL DEFAULT FALSE
);

insert into todo (description) values ('grocery shopping');
insert into todo (description) values ('walk the dog');
insert into todo (description) values ('clean the house');
insert into todo (description, done) values ('do the laundry', 1);
insert into todo (description) values ('take out the trash');
