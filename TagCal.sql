/*
SQLyog Community v13.1.2 (64 bit)
MySQL - 5.5.36 : Database - calender
*********************************************************************
*/

/*!40101 SET NAMES utf8 */;

/*!40101 SET SQL_MODE=''*/;

/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;
CREATE DATABASE /*!32312 IF NOT EXISTS*/`calender` /*!40100 DEFAULT CHARACTER SET utf8 */;

USE `calender`;

/*Table structure for table `cldr` */

DROP TABLE IF EXISTS `cldr`;

CREATE TABLE `cldr` (
  `snum` int(11) NOT NULL,
  `id` varchar(30) NOT NULL,
  `date1` varchar(11) NOT NULL,
  `date2` varchar(11) NOT NULL,
  `title` varchar(50) NOT NULL,
  `tag` varchar(50) DEFAULT NULL,
  `contents` text,
  PRIMARY KEY (`snum`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `cldr` */

insert  into `cldr`(`snum`,`id`,`date1`,`date2`,`title`,`tag`,`contents`) values 
(1,'zv9612','2019-06-29','2019-07-03','일본 여행! 제목을 바꿨습니다!','여행, 일본','내용을 바꿔보았습니다!'),
(2,'zv9612','2019-07-16','2019-07-16','졸작 모임','공부','이날은 졸작 스터디가 있어요~'),
(3,'zv9612','2019-06-23','2019-06-23','수정도 됐으면 좋겠습니다','태그, 용도, 공부','새로고침이됐음 좋겠다'),
(4,'zv9612','2019-06-01','2019-06-02','12','공부','11'),
(5,'zv9612','2019-06-19','2019-06-23','asdqw','공부','qasdq'),
(6,'zv9612','2019-06-23','2019-06-23','asd','','aad'),
(7,'zv9612','2019-06-24','2019-06-24','환전받는날','여행, 은행','환전받고 증명사진도 찍자'),
(8,'zv9612','2019-06-24','2019-06-24','asdawca','용도, 공부','###'),
(9,'zv9612','2019-05-29','2019-06-03','와 벌써 종강이 한달남았어!','잡담','야호'),
(10,'zv9612','2019-06-06','2019-06-09','과제로 과로사..','잡담, 일기','흑흑'),
(11,'zv9612','2019-06-24','2019-06-24','이게 뭔 프로그램이지','잡담','ㅠㅠ'),
(12,'zv9612','2019-08-19','2019-09-12','너가 마지막이야 이땐,, 개강이겠지..','잡담','ㅠㅠㅠ'),
(13,'zv9612','2019-07-20','2019-07-20','이날도 금방오겠지','용도, 잡담','세월..'),
(14,'zv9612','2019-06-27','2019-06-28','긴글이 필요해!','용도, 공부','와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n와 제발\r\n'),
(15,'1','2019-06-24','2019-06-24','1','1','1'),
(16,'zv9612','2019-06-24','2019-06-24','마지막 일정....','끝','이제 정말 끝이야 안녕..');

/*Table structure for table `member` */

DROP TABLE IF EXISTS `member`;

CREATE TABLE `member` (
  `id` varchar(30) NOT NULL,
  `pw` varchar(16) NOT NULL,
  `phone` varchar(13) NOT NULL,
  `mail` varchar(60) NOT NULL,
  PRIMARY KEY (`id`,`phone`,`mail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `member` */

insert  into `member`(`id`,`pw`,`phone`,`mail`) values 
('1','1','111-1111-1111','1@1'),
('q','qwer','010-0000-0000','151515@naver.com'),
('qwer','qwer','010-1111-1111','qwer@naver.com'),
('zv9612','zv84637857','010-4714-5083','zv9612@naver.com');

/*Table structure for table `savevalue` */

DROP TABLE IF EXISTS `savevalue`;

CREATE TABLE `savevalue` (
  `value1` int(11) DEFAULT NULL,
  `value2` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `savevalue` */

insert  into `savevalue`(`value1`,`value2`) values 
(17,1);

/*Table structure for table `tag` */

DROP TABLE IF EXISTS `tag`;

CREATE TABLE `tag` (
  `tagvalue` varchar(10) NOT NULL,
  `id` varchar(30) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

/*Data for the table `tag` */

insert  into `tag`(`tagvalue`,`id`) values 
('여행','zv9612'),
('일본','zv9612'),
('태그','zv9612'),
('용도','zv9612'),
('은행','zv9612'),
('공부','zv9612'),
('잡담','zv9612'),
('일기','zv9612'),
('1','1'),
('끝','zv9612');

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;
