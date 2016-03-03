# Table: Chairs #
| **Column Name** | **Data Type** | |
|:----------------|:--------------|:|
| id              | int           |Primary |
| Room            | int           | FK->Rooms.id |
| posX            | int           | |
| posY            | int           | |
| leftHanded      | boolean       | |
| fbPosition      | int           | |
| lrPosition      | int           | |
| nonChair        | boolean       | |
| mustBeEmpty     | boolean       | |
| seatNumber      | string        | |

# Table: Rooms #
| **Column Name** | **Data Type** | |
|:----------------|:--------------|:|
| id              | int           | Primary |
| RoomType        | int           | FK->RoomType.id |
| roomName        | string        | |
| location        | string        | |
| description     | string        | |

# Table: RoomType #
| id | int | Primary |
|:---|:----|:--------|
| roomTypeName | string |         |

# Table: Students #
| id | int | Primary |
|:---|:----|:--------|
| firstName | string |         |
| lastName | string |         |
| section | int |         |
| leftHanded | boolean |         |
| visionImpairment| boolean |         |
| isEnrolled | boolean |         |

# Table: SeatAssignments #
| id | int | Primary |
|:---|:----|:--------|
| Student | int | FK->Student.id |
| Chair | int | FK->Chair.id |

# Table: RoomAssignments #
| id | int | Primary |
|:---|:----|:--------|
| Room | int | FK->Room.id |
| Student | int | FK->Student.id |