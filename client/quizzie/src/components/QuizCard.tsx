import React from 'react';

interface Quiz {
    quizName: string;
    quizPicture: string;
}

function QuizCard(props: Quiz) {
    return (
        <div>
            <div className='bg-white flex justify-between w-1/3 border p-4 rounded-lg'>
                <div className='flex flex-col md:w-1/3 pb-4'>
                    <img src={props.quizPicture} alt="Quiz Picture" />
                    <p className='text-gray-600'>{props.quizName}</p>
                </div>
            </div>
        </div>
    );
};

export default QuizCard;
