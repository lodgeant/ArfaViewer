﻿DELETE FROM COMPOSITEPART

INSERT INTO COMPOSITEPART
(
	ID,
	LDRAW_REF,
	LDRAW_DESCRIPTION,
	PARENT_LDRAW_REF,
	LDRAW_COLOUR_ID,
	POS_X,
	POS_Y,
	POS_Z,
	ROT_X,
	ROT_Y,
	ROT_Z
)
VALUES
(1,'4592','Hinge Control Stick Base','4592c01',-1,0,-0.0644,0,0,0,0),
(2,'4593','Hinge Control Stick','4592c01',0,0,0.8446,0,0,0,0),
(3,'3828','~Car Steering Wheel','3829c01',0,0,1.017625,0.05650092,-53,0,0),
(4,'3829a','~Car Steering Wheel Stand','3829c01',-1,0,0.3768234,0.1041753,0,0,0),
(5,'54701p01','~Plane Aft Section  8 x 16 x  7 Top with Blue Stripes Pattern','54701p01c01',-1,0,-1.506984,-3.999195,0,0,0),
(6,'54702','~Plane Aft Section  8 x 16 x  7 Bottom','54701p01c01',71,0,-5.286277,-1.225205,0,0,0),
(7,'54703','~Plane Aft Section  8 x 16 x  7 Tip','54701p01c01',-1,0,-0.7700346,-12.11775,0,0,0),
(8,'u9288','~Screw 23.75 x  6.5 Dome Cruciform','54701p01c01',0,0,-2.17631,-9.659688,0,0,0),
(9,'u9288','~Screw 23.75 x  6.5 Dome Cruciform','54701p01c01',0,2.059308,-3.43631,-0.9096883,0,0,0),
(10,'u9288','~Screw 23.75 x  6.5 Dome Cruciform','54701p01c01',0,-2.058566,-3.43631,-0.9096549,0,0,0),
(11,'3496','~Tap  1 x  2 Base','3496c01',-1,0,-0.01647059,0,0,0,0),
(12,'3278','~Tap  1 x  2 Spout','3496c01',7,0,0.6451139,0.2538055,0,0,0),
(13,'3680','Turntable  2 x  2 Plate Base','3680c02',-1,0,-0.2060377,0,0,0,0),
(14,'3679','Turntable  2 x  2 Plate Top','3680c02',71,0,-0.1011375,0,0,0,0),
(15,'4863','Window  1 x  4 x  2 Plane','4863c01',-1,0,-0.7742776,0.0871934,0,0,0),
(16,'4862','Glass for Window  1 x  2 x  2 Plane','4863c01',40,-0.8,-0.7446154,0.2892307,0,0,0),
(17,'4862','Glass for Window  1 x  2 x  2 Plane','4863c01',40,0.8,-0.7446154,0.2892307,0,0,0),
(18,'2377','Window  1 x  2 x  2 Plane','2377c01',-1,0,-0.706145,0.08581001,0,0,0),
(19,'4862','Glass for Window  1 x  2 x  2 Plane','2377c01',40,0,-0.7446154,0.2892307,0,0,0),
(20,'54092p02','~Plane Front  8 x 16 x  5 with Blue Airline Bird Pattern','54092p02c01',-1,0,-1.757707,2.105772,0,0,0),
(21,'54760','~Glass for Plane Front  8 x 16 x  5','54092p02c01',40,0,-1.962182,4.259689,0,0,0),
(22,'u9288','~Screw 23.75 x  6.5 Dome Cruciform','54092p02c01',0,0,-3.57255,7.792311,0,0,0),
(23,'u9288','~Screw 23.75 x  6.5 Dome Cruciform','54092p02c01',0,2.643512,-2.42255,2.141987,0,0,0),
(24,'u9288','~Screw 23.75 x  6.5 Dome Cruciform','54092p02c01',0,-2.642888,-2.42255,2.141987,0,0,0),
(25,'973pq7','Minifig Torso with Safari Shirt, Tan Bandana & Compass Pattern','PHA002Torso',28,0,0,0,0,-90,0),
(26,'3818','Minifig Arm Right','PHA002Torso',28,-0.05,-0.04,0.765,0,-90,10),
(27,'3819','Minifig Arm Left','PHA002Torso',28,-0.05,-0.04,-0.765,0,-90,-10),
(28,'3820','Minifig Hand','PHA002Torso',14,-0.537,-0.6,0.95,43.683,-82.905,0),
(29,'3820','Minifig Hand','PHA002Torso',14,-0.537,-0.6,-0.95,43.739,-96.248,0),
(30,'3815','Minifig Hips','PHA002Legs',19,0,-0.06516036,0,0,0,0),
(31,'3816','Minifig Leg Right','PHA002Legs',19,0.4,-0.9,0.007,0,0,0),
(32,'3817','Minifig Leg Left','PHA002Legs',19,-0.4,-0.9,0.007,0,0,0),
(33,'973pq4','Minifig Torso with Tank Top, Braces & Yellow Skin Pattern','PHA004Torso',15,-0.11,0.04,0,0,-90,0),
(34,'3818','Minifig Arm Right','PHA004Torso',14,-0.05,-0.04,0.765,0,-90,10),
(35,'3819','Minifig Arm Left','PHA004Torso',14,-0.05,-0.04,-0.765,0,-90,-10),
(36,'3820','Minifig Hand','PHA004Torso',14,-0.537,-0.6,0.95,43.7,-82.9,0),
(37,'3820','Minifig Hand','PHA004Torso',14,-0.537,-0.6,-0.95,43.7,-96.25,0),
(38,'3815','Minifig Hips','PHA004Legs',308,0,-0.06516036,0,0,0,0),
(39,'3816','Minifig Leg Right','PHA004Legs',308,0.4,-0.9,0.007,0,0,0),
(40,'3817','Minifig Leg Left','PHA004Legs',308,-0.4,-0.9,0.007,0,0,0),
(41,'973pq0','Minifig Torso with Bandage and Gold Necklace Pattern','PHA005Torso',71,0,0,0,0,-90,0),
(42,'3818','Minifig Arm Right','PHA005Torso',71,-0.05,-0.04,0.765,0,-90,10),
(43,'3819','Minifig Arm Left','PHA005Torso',71,-0.05,-0.04,-0.765,0,-90,-10),
(44,'3820','Minifig Hand','PHA005Torso',72,-0.537,-0.6,0.95,43.7,-82.9,0),
(45,'3820','Minifig Hand','PHA005Torso',72,-0.537,-0.6,-0.95,43.7,-96.25,0),
(46,'3815pq0','Minifig Hips with Gold Belt and DarkBlue Loincloth Pattern','PHA005Legs',71,0,-0.06516036,0,0,0,0),
(47,'3816pq0','Minifig Leg Right with DarkBlue Loincloth Pattern','PHA005Legs',71,0.4,-0.9,0.007,0,0,0),
(48,'3817pq0','Minifig Leg Left with DarkBlue Loincloth Pattern','PHA005Legs',71,-0.4,-0.9,0.007,0,0,0),
(49,'3815','Minifig Hips','PHA009Legs',28,0,-0.06516036,0,0,0,0),
(50,'3816','Minifig Leg Right','PHA009Legs',28,0.4,-0.9,0.007,0,0,0),
(51,'3817','Minifig Leg Left','PHA009Legs',28,-0.4,-0.9,0.007,0,0,0),
(52,'973pq3','Minifig Torso with Waistcoat, White Shirt and Bandolier Pattern','PHA009Torso',19,0,0,0,0,-90,0),
(53,'3818','Minifig Arm Right','PHA009Torso',19,-0.05,-0.04,0.765,0,-90,10),
(54,'3819','Minifig Arm Left','PHA009Torso',19,-0.05,-0.04,-0.765,0,-90,-10),
(55,'3820','Minifig Hand','PHA009Torso',14,-0.537,-0.6,0.95,43.7,-82.9,0),
(56,'3820','Minifig Hand','PHA009Torso',14,-0.537,-0.6,-0.95,43.7,-96.25,0),
(57,'973p8u','Minifig Torso with Six Button Suit, Red Tie, and Airplane Pattern','AIR022Torso',0,0,0,0,0,-90,0),
(58,'3818','Minifig Arm Right','AIR022Torso',0,-0.05,-0.04,0.765,0,-90,10),
(59,'3819','Minifig Arm Left','AIR022Torso',0,-0.05,-0.04,-0.765,0,-90,-10),
(60,'3820','Minifig Hand','AIR022Torso',14,-0.537,-0.6,0.95,43.7,-82.9,0),
(61,'3820','Minifig Hand','AIR022Torso',14,-0.537,-0.6,-0.95,43.7,-96.25,0),
(62,'3815','Minifig Hips','AIR022Legs',0,0,-0.06516036,0,0,0,0),
(63,'3816','Minifig Leg Right','AIR022Legs',0,0.4,-0.9,0.007,0,0,0),
(64,'3817','Minifig Leg Left','AIR022Legs',0,-0.4,-0.9,0.007,0,0,0),
(65,'973','Minifig Torso','AIR023Torso',1,0,0,0,0,-90,0),
(66,'3818','Minifig Arm Right','AIR023Torso',1,-0.05,-0.04,0.765,0,-90,10),
(67,'3819','Minifig Arm Left','AIR023Torso',1,-0.05,-0.04,-0.765,0,-90,-10),
(68,'3820','Minifig Hand','AIR023Torso',14,-0.537,-0.6,0.95,43.7,-82.9,0),
(69,'3820','Minifig Hand','AIR023Torso',14,-0.537,-0.6,-0.95,43.7,-96.25,0),
(70,'3815','Minifig Hips','AIR023Legs',1,0,-0.06516036,0,0,0,0),
(71,'3816','Minifig Leg Right','AIR023Legs',1,0.4,-0.9,0.007,0,0,0),
(72,'3817','Minifig Leg Left','AIR023Legs',1,-0.4,-0.9,0.007,0,0,0),
(73,'973p6j','Minifig Torso with Jacket, Zippered Pockets and Classic Space Logo Pattern','TWN027Torso',4,0,0,0,0,-90,0),
(74,'3818','Minifig Arm Right','TWN027Torso',4,-0.05,-0.04,0.765,0,-90,10),
(75,'3819','Minifig Arm Left','TWN027Torso',4,-0.05,-0.04,-0.765,0,-90,-10),
(76,'3820','Minifig Hand','TWN027Torso',14,-0.537,-0.6,0.95,43.7,-82.9,0),
(77,'3820','Minifig Hand','TWN027Torso',14,-0.537,-0.6,-0.95,43.7,-96.25,0),
(78,'3815','Minifig Hips','TWN027Legs',1,0,-0.06516036,0,0,0,0),
(79,'3816','Minifig Leg Right','TWN027Legs',1,0.4,-0.9,0.007,0,0,0),
(80,'3817','Minifig Leg Left','TWN027Legs',1,-0.4,-0.9,0.007,0,0,0),
(81,'973p8j','Minifig Torso with Town Vest with Pockets and Striped Tie Pattern','TWN028Torso',0,0,0,0,0,-90,0),
(82,'3818','Minifig Arm Right','TWN028Torso',15,-0.05,-0.04,0.765,0,-90,10),
(83,'3819','Minifig Arm Left','TWN028Torso',15,-0.05,-0.04,-0.765,0,-90,-10),
(84,'3820','Minifig Hand','TWN028Torso',14,-0.537,-0.6,0.95,43.7,-82.9,0),
(85,'3820','Minifig Hand','TWN028Torso',14,-0.537,-0.6,-0.95,43.7,-96.25,0),
(86,'3815','Minifig Hips','TWN028Legs',72,0,-0.06516036,0,0,0,0),
(87,'3816','Minifig Leg Right','TWN028Legs',72,0.4,-0.9,0.007,0,0,0),
(88,'3817','Minifig Leg Left','TWN028Legs',72,-0.4,-0.9,0.007,0,0,0),
(89,'973pq0','Minifig Torso with Bandage and Gold Necklace Pattern','PHA003Torso',71,0,0,0,0,-90,0),
(90,'3818','Minifig Arm Right','PHA003Torso',71,-0.05,-0.04,0.765,0,-90,10),
(91,'3819','Minifig Arm Left','PHA003Torso',71,-0.05,-0.04,-0.765,0,-90,-10),
(92,'3820','Minifig Hand','PHA003Torso',72,-0.537,-0.6,0.95,43.7,-82.9,0),
(93,'3820','Minifig Hand','PHA003Torso',72,-0.537,-0.6,-0.95,43.7,-96.25,0),
(94,'3815pq0','Minifig Hips with Gold Belt and DarkBlue Loincloth Pattern','PHA003Legs',71,0,-0.06516036,0,0,0,0),
(95,'3816pq0','Minifig Leg Right with DarkBlue Loincloth Pattern','PHA003Legs',71,0.4,-0.9,0.007,0,0,0),
(96,'3817pq0','Minifig Leg Left with DarkBlue Loincloth Pattern','PHA003Legs',71,-0.4,-0.9,0.007,0,0,0),
(97,'973','Minifig Torso','PHA001Torso',15,0,0,0,0,-90,0),
(98,'3818','Minifig Arm Right','PHA001Torso',15,-0.05,-0.04,0.765,0,-90,10),
(99,'3819','Minifig Arm Left','PHA001Torso',15,-0.05,-0.04,-0.765,0,-90,-10),
(100,'3820','Minifig Hand','PHA001Torso',15,-0.537,-0.6,0.95,43.7,-82.9,0),
(101,'3820','Minifig Hand','PHA001Torso',15,-0.537,-0.6,-0.95,43.7,-96.25,0),
(102,'3815','Minifig Hips','PHA001Legs',15,0,-0.06516036,0,0,0,0),
(103,'3816','Minifig Leg Right','PHA001Legs',15,0.4,-0.9,0.007,0,0,0),
(104,'3817','Minifig Leg Left','PHA001Legs',15,-0.4,-0.9,0.007,0,0,0),
(105,'973pq2','Minifig Torso with DarkBlue Muscles and Gold Necklace Pattern','PHA008Torso',0,-0.1,0.15,0,0,-90,0),
(106,'3818','Minifig Arm Right','PHA008Torso',0,-0.05,-0.04,0.765,0,-90,10),
(107,'3819','Minifig Arm Left','PHA008Torso',0,-0.05,-0.04,-0.765,0,-90,-10),
(108,'3820','Minifig Hand','PHA008Torso',0,-0.537,-0.6,0.95,43.7,-82.9,0),
(109,'3820','Minifig Hand','PHA008Torso',0,-0.537,-0.6,-0.95,43.7,-96.25,0),
(110,'3815pq2','Minifig Hips with Gold O-Belt and Loincloth Pattern','PHA008Legs',0,0,-0.07,0.15,0,0,0),
(111,'3816pq1','Minifig Leg Right with DarkBlue and Gold Loincloth Pattern','PHA008Legs',0,0.36,-0.7,0.1,0,0,0),
(112,'3817pq1','Minifig Leg Left with DarkBlue and Gold Loincloth Pattern','PHA008Legs',0,-0.38,-0.7,0.1,0,0,0),
(113,'2527','Minifig Cannon  2 x  4 Base','2527c01',-1,0,0.2534,0.2086,0,0,0),
(114,'518','Minifig Cannon  2 x  8 Non-Shooting','2527c01',8,0,1.2608,0.8232,0,0,0),
(115,'973pa5','Minifig Torso with Bomber Jacket, Belt, Black Shirt Pattern ','ADV009Torso',6,-0.24,-0.05,0,0,-90,0),
(116,'3818','Minifig Arm Right','ADV009Torso',6,-0.05,-0.04,0.765,0,-90,10),
(117,'3819','Minifig Arm Left','ADV009Torso',6,-0.05,-0.04,-0.765,0,-90,-10),
(118,'3820','Minifig Hand','ADV009Torso',14,-0.537,-0.6,0.95,43.7,-82.9,0),
(119,'3820','Minifig Hand','ADV009Torso',14,-0.537,-0.6,-0.95,43.7,-96.25,0),
(120,'3815','Minifig Hips','ADV009Legs',0,0,-0.0652,0,0,0,0),
(121,'3816','Minifig Leg Right','ADV009Legs',0,0.4,-0.9,0.007,0,0,0),
(122,'3817','Minifig Leg Left','ADV009Legs',0,-0.4,-0.9,0.007,0,0,0),
(123,'2429','Hinge Plate  1 x  4 Base','2429c01',-1,0.5068,-0.1159,-0.2581,0,0,0),
(124,'2430','Hinge Plate  1 x  4 Top','2429c01',-1,-0.2892,-0.1707,-0.1498,0,0,0),
(125,'973d01','Minifig Torso with "TINE" Stickers','ADV040Torso',15,-0.004,0.071,0.035,0,90,0),
(126,'3818','Minifig Arm Right','ADV040Torso',15,-0.05,-0.04,0.765,0,-90,10),
(127,'3819','Minifig Arm Left','ADV040Torso',15,-0.05,-0.04,-0.765,0,-90,-10),
(128,'3820','Minifig Hand','ADV040Torso',14,-0.537,-0.6,0.95,43.7,-82.9,0),
(129,'3820','Minifig Hand','ADV040Torso',14,-0.537,-0.6,-0.95,43.7,-96.25,0),
(130,'3815','Minifig Hips','ADV040Legs',2,0,-0.0652,0,0,0,0),
(131,'3816','Minifig Leg Right','ADV040Legs',2,0.4,-0.9,0.007,0,0,0),
(132,'3817','Minifig Leg Left','ADV040Legs',2,-0.4,-0.9,0.007,0,0,0),
(133,'973','Minifig Torso','ADV038Torso',0,0,0,0,0,-90,0),
(134,'3818','Minifig Arm Right','ADV038Torso',0,-0.05,-0.04,0.765,0,-90,10),
(135,'3819','Minifig Arm Left','ADV038Torso',0,-0.05,-0.04,-0.765,0,-90,-10),
(136,'3820','Minifig Hand','ADV038Torso',14,-0.537,-0.6,0.95,43.7,-82.9,0),
(137,'3820','Minifig Hand','ADV038Torso',14,-0.537,-0.6,-0.95,43.7,-96.25,0),
(138,'3815','Minifig Hips','ADV038Legs',0,0,-0.0652,0,0,0,0),
(139,'3816','Minifig Leg Right','ADV038Legs',0,0.4,-0.9,0.007,0,0,0),
(140,'3817','Minifig Leg Left','ADV038Legs',0,-0.4,-0.9,0.007,0,0,0),
(141,'973','Minifig Torso','ADV039Torso',19,0,0,0,0,-90,0),
(142,'3818','Minifig Arm Right','ADV039Torso',19,-0.05,-0.04,0.765,0,-90,10),
(143,'3819','Minifig Arm Left','ADV039Torso',19,-0.05,-0.04,-0.765,0,-90,-10),
(144,'3820','Minifig Hand','ADV039Torso',0,-0.537,-0.6,0.95,43.7,-82.9,0),
(145,'3820','Minifig Hand','ADV039Torso',72,-0.537,-0.6,-0.95,43.7,-96.25,0),
(146,'3815','Minifig Hips','ADV039Legs',71,0,-0.0652,0,0,0,0),
(147,'3816','Minifig Leg Right','ADV039Legs',71,0.4,-0.9,0.007,0,0,0),
(148,'3817','Minifig Leg Left','ADV039Legs',71,-0.4,-0.9,0.007,0,0,0),
(149,'973d03','Minifig Torso with White Buttons and Police Badge Plain Sticker','ADV006Torso',15,-0.076,0,0,0,-90,0),
(150,'3818','Minifig Arm Right','ADV006Torso',15,-0.05,-0.04,0.765,0,-90,10),
(151,'3819','Minifig Arm Left','ADV006Torso',15,-0.05,-0.04,-0.765,0,-90,-10),
(152,'3820','Minifig Hand','ADV006Torso',14,-0.537,-0.6,0.95,43.7,-82.9,0),
(153,'3820','Minifig Hand','ADV006Torso',14,-0.537,-0.6,-0.95,43.7,-96.25,0),
(154,'3815','Minifig Hips','ADV006Legs',2,0,-0.0652,0,0,0,0),
(155,'3816','Minifig Leg Right','ADV006Legs',2,0.4,-0.9,0.007,0,0,0),
(156,'3817','Minifig Leg Left','ADV006Legs',2,-0.4,-0.9,0.007,0,0,0),
(157,'973','Minifig Torso','SW0666Torso',15,0,0,0,0,-90,0),
(158,'3818','Minifig Arm Right','SW0666Torso',15,-0.05,-0.04,0.765,0,-90,10),
(159,'3819','Minifig Arm Left','SW0666Torso',15,-0.05,-0.04,-0.765,0,-90,-10),
(160,'3820','Minifig Hand','SW0666Torso',15,-0.537,-0.6,0.95,43.7,-82.9,0),
(161,'3820','Minifig Hand','SW0666Torso',15,-0.537,-0.6,-0.95,43.7,-96.25,0),
(162,'3815','Minifig Hips','SW0666Legs',15,0,-0.0652,0,0,0,0),
(163,'3816','Minifig Leg Right','SW0666Legs',15,0.4,-0.9,0.007,0,0,0),
(164,'3817','Minifig Leg Left','SW0666Legs',15,-0.4,-0.9,0.007,0,0,0),
(165,'973','Minifig Torso','SW0667Torso',15,0,0,0,0,-90,0),
(166,'3818','Minifig Arm Right','SW0667Torso',15,-0.05,-0.04,0.765,0,-90,10),
(167,'3819','Minifig Arm Left','SW0667Torso',15,-0.05,-0.04,-0.765,0,-90,-10),
(168,'3820','Minifig Hand','SW0667Torso',15,-0.537,-0.6,0.95,43.7,-82.9,0),
(169,'3820','Minifig Hand','SW0667Torso',15,-0.537,-0.6,-0.95,43.7,-96.25,0),
(170,'3815','Minifig Hips','SW0667Legs',15,0,-0.0652,0,0,0,0),
(171,'3816','Minifig Leg Right','SW0667Legs',15,0.4,-0.9,0.007,0,0,0),
(172,'3817','Minifig Leg Left','SW0667Legs',15,-0.4,-0.9,0.007,0,0,0),
(173,'973','Minifig Torso','SW0668Torso',19,0,0,0,0,-90,0),
(174,'3818','Minifig Arm Right','SW0668Torso',19,-0.05,-0.04,0.765,0,-90,10),
(175,'3819','Minifig Arm Left','SW0668Torso',19,-0.05,-0.04,-0.765,0,-90,-10),
(176,'3820','Minifig Hand','SW0668Torso',78,-0.537,-0.6,0.95,43.7,-82.9,0),
(177,'3820','Minifig Hand','SW0668Torso',78,-0.537,-0.6,-0.95,43.7,-96.25,0),
(178,'3815','Minifig Hips','SW0668Legs',326,0,-0.0652,0,0,0,0),
(179,'3816','Minifig Leg Right','SW0668Legs',326,0.4,-0.9,0.007,0,0,0),
(180,'3817','Minifig Leg Left','SW0668Legs',326,-0.4,-0.9,0.007,0,0,0),
(181,'973','Minifig Torso','SW0669Torso',28,0,0,0,0,-90,0),
(182,'3818','Minifig Arm Right','SW0669Torso',28,-0.05,-0.04,0.765,0,-90,10),
(183,'3819','Minifig Arm Left','SW0669Torso',28,-0.05,-0.04,-0.765,0,-90,-10),
(184,'3820','Minifig Hand','SW0669Torso',78,-0.537,-0.6,0.95,43.7,-82.9,0),
(185,'3820','Minifig Hand','SW0669Torso',78,-0.537,-0.6,-0.95,43.7,-96.25,0),
(186,'3815','Minifig Hips','SW0669Legs',308,0,-0.0652,0,0,0,0),
(187,'3816','Minifig Leg Right','SW0669Legs',308,0.4,-0.9,0.007,0,0,0),
(188,'3817','Minifig Leg Left','SW0669Legs',308,-0.4,-0.9,0.007,0,0,0),
(189,'973','Minifig Torso','SW0684Torso',179,0,0,0,0,-90,0),
(190,'3818','Minifig Arm Right','SW0684Torso',179,-0.05,-0.04,0.765,0,-90,10),
(191,'3819','Minifig Arm Left','SW0684Torso',179,-0.05,-0.04,-0.765,0,-90,-10),
(192,'3820','Minifig Hand','SW0684Torso',72,-0.537,-0.6,0.95,43.7,-82.9,0),
(193,'3820','Minifig Hand','SW0684Torso',72,-0.537,-0.6,-0.95,43.7,-96.25,0),
(194,'3815','Minifig Hips','SW0684Legs',179,0,-0.0652,0,0,0,0),
(195,'3816','Minifig Leg Right','SW0684Legs',179,0.4,-0.9,0.007,0,0,0),
(196,'3817','Minifig Leg Left','SW0684Legs',179,-0.4,-0.9,0.007,0,0,0),
(197,'973','Minifig Torso','SW0655Torso',72,0,0,0,0,-90,0),
(198,'3818','Minifig Arm Right','SW0655Torso',72,-0.05,-0.04,0.765,0,-90,10),
(199,'3819','Minifig Arm Left','SW0655Torso',72,-0.05,-0.04,-0.765,0,-90,-10),
(200,'3820','Minifig Hand','SW0655Torso',0,-0.537,-0.6,0.95,43.7,-82.9,0),
(201,'3820','Minifig Hand','SW0655Torso',0,-0.537,-0.6,-0.95,43.7,-96.25,0),
(202,'3815','Minifig Hips','SW0655Legs',72,0,-0.0652,0,0,0,0),
(203,'3816','Minifig Leg Right','SW0655Legs',72,0.4,-0.9,0.007,0,0,0),
(204,'3817','Minifig Leg Left','SW0655Legs',72,-0.4,-0.9,0.007,0,0,0),
(205,'973','Minifig Torso','SW0677Torso',71,0,0,0,0,-90,0),
(206,'3818','Minifig Arm Right','SW0677Torso',71,-0.05,-0.04,0.765,0,-90,10),
(207,'3819','Minifig Arm Left','SW0677Torso',71,-0.05,-0.04,-0.765,0,-90,-10),
(208,'3820','Minifig Hand','SW0677Torso',78,-0.537,-0.6,0.95,43.7,-82.9,0),
(209,'3820','Minifig Hand','SW0677Torso',78,-0.537,-0.6,-0.95,43.7,-96.25,0),
(210,'3815','Minifig Hips','SW0677Legs',28,0,-0.0652,0,0,0,0),
(211,'3816','Minifig Leg Right','SW0677Legs',28,0.4,-0.9,0.007,0,0,0),
(212,'3817','Minifig Leg Left','SW0677Legs',28,-0.4,-0.9,0.007,0,0,0),
(213,'15403','Plate  1 x  2 with Mini Shooting Blaster','15403c01',-1,0,0.2494,0.5454,0,0,0),
(214,'15392','Minifig Gun Shooting Blaster Trigger','15403c01',72,0.0043,0.6163,0.998,0,0,0),
(215,'4254','~Technic Shock Absorber  6.5L Piston Rod','76537',0,0,-3.8173,0,0,0,0),
(216,'4255','~Technic Shock Absorber  6.5L Cylinder','76537',-1,0,-0.8078,0,0,0,0),
(217,'22977','~Spring for Technic Shock Absorber  6.5L Extra Stiff','76537',494,0.0071,-3.1414,0,0,0,0),
(218,'422','Plate  2 x  2 with Axle Brackets','122c01',-1,0,-0.1307,0,0,0,0),
(219,'u9132c01','~Axle Steel  4 x  72 LDU with Two Wheels  4 x  8','122c01',4,0,-0.24,0,0,0,0),
(220,'3815c01','Minifig Hips and Legs','970c00',-1,-1,0,0,0,0,0),
(221,'3815','Minifig Hips','3815c01',-1,0,-0.0663,0,0,0,0),
(222,'3816','Minifig Leg Right','3815c01',-1,0.3501,-0.7096,-0.0854,0,0,0),
(223,'3817','Minifig Leg Left','3815c01',-1,-0.3501,-0.7096,-0.0854,0,0,0),
(224,'973','Minifig Torso','973c01',-1,0,-0.2944,0,0,0,0),
(225,'3818','Minifig Arm Right','973c01',-1,0.6523,-0.5092,0.0473,0,0,10),
(226,'3819','Minifig Arm Left','973c01',-1,-0.6523,-0.5092,0.0473,0,0,-10),
(227,'3820','Minifig Hand','973c01',14,0.9662,-1.1779,0.5378,43.7,8,0),
(228,'3820','Minifig Hand','973c01',14,-0.9659,-1.1779,0.5378,43.7,-8,0),
(229,'3815','Minifig Hips','PHA000Legs',15,0,-0.0663,0,0,0,0),
(230,'3816','Minifig Leg Right','PHA000Legs',15,0.3501,-0.7096,-0.0854,0,0,0),
(231,'3817','Minifig Leg Left','PHA000Legs',15,-0.3501,-0.7096,-0.0854,0,0,0),
(232,'973','Minifig Torso','PHA000Torso',15,0,-0.2944,0,0,0,0),
(233,'3818','Minifig Arm Right','PHA000Torso',15,0.6523,-0.5092,0.0473,0,0,10),
(234,'3819','Minifig Arm Left','PHA000Torso',15,-0.6523,-0.5092,0.0473,0,0,-10),
(235,'3820','Minifig Hand','PHA000Torso',14,0.9662,-1.1779,0.5378,43.7,8,0),
(236,'3820','Minifig Hand','PHA000Torso',14,-0.9659,-1.1779,0.5378,43.7,-8,0),
(237,'3815','Minifig Hips','PLN106aLegs',0,0,-0.0663,0,0,0,0),
(238,'3816','Minifig Leg Right','PLN106aLegs',0,0.3501,-0.7096,-0.0854,0,0,0),
(239,'3817','Minifig Leg Left','PLN106aLegs',0,-0.3501,-0.7096,-0.0854,0,0,0),
(240,'973d03','Minifig Torso','PLN106aTorso',0,-0.0518,-0.4174,0.1981,0,0,0),
(241,'3818','Minifig Arm Right','PLN106aTorso',0,0.6523,-0.5092,0.0473,0,0,10),
(242,'3819','Minifig Arm Left','PLN106aTorso',0,-0.6523,-0.5092,0.0473,0,0,-10),
(243,'3820','Minifig Hand','PLN106aTorso',14,0.9662,-1.1779,0.5378,43.7,8,0),
(244,'3820','Minifig Hand','PLN106aTorso',14,-0.9659,-1.1779,0.5378,43.7,-8,0),
(245,'3815','Minifig Hips','PLN106Legs',0,0,-0.0663,0,0,0,0),
(246,'3816','Minifig Leg Right','PLN106Legs',0,0.3501,-0.7096,-0.0854,0,0,0),
(247,'3817','Minifig Leg Left','PLN106Legs',0,-0.3501,-0.7096,-0.0854,0,0,0),
(248,'973','Minifig Torso','PLN106Torso',0,0,-0.2944,0,0,0,0),
(249,'3818','Minifig Arm Right','PLN106Torso',0,0.6523,-0.5092,0.0473,0,0,10),
(250,'3819','Minifig Arm Left','PLN106Torso',0,-0.6523,-0.5092,0.0473,0,0,-10),
(251,'3820','Minifig Hand','PLN106Torso',14,0.9662,-1.1779,0.5378,43.7,8,0),
(252,'3820','Minifig Hand','PLN106Torso',14,-0.9659,-1.1779,0.5378,43.7,-8,0),
(253,'3815pq1','Minifig Hips','PHA007Legs',28,0.0001,-0.2301,0.1935,0,0,0),
(254,'3816pq1','Minifig Leg Right','PHA007Legs',28,0.3574,-0.7427,0.0791,0,0,0),
(255,'3817pq1','Minifig Leg Left','PHA007Legs',28,-0.3759,-0.7434,0.0825,0,0,0),
(256,'973','Minifig Torso','PHA007Torso',28,0,-0.2944,0,0,0,0),
(257,'3818','Minifig Arm Right','PHA007Torso',28,0.6523,-0.5092,0.0473,0,0,10),
(258,'3819','Minifig Arm Left','PHA007Torso',28,-0.6523,-0.5092,0.0473,0,0,-10),
(259,'3820','Minifig Hand','PHA007Torso',72,0.9662,-1.1779,0.5378,43.7,8,0),
(260,'3820','Minifig Hand','PHA007Torso',72,-0.9659,-1.1779,0.5378,43.7,-8,0)
;