import React from 'react';
import Link from 'next/link';

interface Quiz {
    quizName: string;
    quizPicture: string;
}

function QuizCard(props: Quiz) {
    return (
        <div>
            <Link href="">
                <div className='bg-white flex justify-between w-full border p-2 rounded-lg'>
                    <div className='flex flex-col md:w-full pb-4'>
                        <img src={props.quizPicture} alt="Quiz Picture" />
                        <p className='text-gray-600'>{props.quizName}</p>
                    </div>
                </div>
            </Link>
        </div>
    );
};

export default QuizCard;
