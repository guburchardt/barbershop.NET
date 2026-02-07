         erDiagram
  EMPLOYEES ||--o{ APPOINTMENTS : has
  CLIENTS   ||--o{ APPOINTMENTS : books
  EMPLOYEES ||--o{ TIME_BLOCKS  : blocks

  EMPLOYEES {
    uuid        id PK
    varchar(120) full_name
    boolean     is_active
    timestamptz created_at
    timestamptz updated_at
  }

  CLIENTS {
    uuid         id PK
    varchar(120) full_name
    varchar(20)  phone
    varchar(160) email
    boolean      is_active
    timestamptz  created_at
    timestamptz  updated_at
  }

  APPOINTMENTS {
    uuid         id PK
    uuid         employee_id FK
    uuid         client_id FK
    timestamptz  start_at
    timestamptz  end_at
    smallint     status
    timestamptz  cancelled_at
    varchar(250) cancel_reason
    timestamptz  created_at
    timestamptz  updated_at
  }

  TIME_BLOCKS {       
    uuid         id PK
    uuid         employee_id FK  "nullable (blocks all if null)"
    timestamptz  start_at
    timestamptz  end_at
    varchar(120) reason
    timestamptz  created_at
  }
